require_relative "base_generator.rb"
require_relative "api_request_resolver.rb"

class RequestsGenerator < BaseGenerator
  attr_reader :output

  def initialize(api_name, data, interface_mode)
    super()
    @data = data
    @api_name = api_name
    @interface_mode = interface_mode
    generate
  end
  
private
  def write_methods(requests)
    if @interface_mode
      requests.each do |r|
        write r.interface_method
      end
    else
      requests.each do |r|
        write r.class_method
      end
    end
  end

  def generate
    @output = ""

    requests = @data["paths"].map {|request_path, request| resolve_api_request(@data["basePath"] + request_path, request["get"])}.compact

    write_using_namespace "PatchKit.Core"
    write_using_namespace "PatchKit.Core.Collections.Immutable"
    write_using_namespace models_namespace(@api_name)
    write_using_namespace "Newtonsoft.Json"
    write_using_namespace "System.Collections.Generic"
    write nil
    write_block "namespace PatchKit.Api" do
      if @interface_mode
        class_signature = "public partial interface I#{@api_name}ApiConnection"
      else
        class_signature = "public partial class #{@api_name}ApiConnection"
      end
      write_block class_signature do
        write_methods requests
      end
    end
  end
end
