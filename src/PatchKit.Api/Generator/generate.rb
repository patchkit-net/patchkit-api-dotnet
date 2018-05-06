require 'json'
require 'fileutils'
require_relative 'api_model_resolver.rb'
require_relative 'requests_generator.rb'

def swagger_filename(name)
  name.nil? ? "swagger.json" : "swagger-#{name.downcase}.json"
end

def models_dirname(name)
  name.nil? ? "Models" : "Models/#{name}"
end

def generate(name)
  data = JSON.parse(File.read(swagger_filename(name)))

  models_namespace = "PatchKit.Api.Models"
  models_namespace += ".#{name}" unless name.nil?

  data["definitions"].each do |model_name, model|
    next if model["type"] != "object"

    dirname = models_dirname(name)
    FileUtils::mkdir_p(dirname)
    File.open("#{dirname}/#{model_name}.cs", "w") do |output|
      cw = CodeWriter.new
      cw.write_using_namespace "Newtonsoft.Json"
      cw.write_using_namespace "PatchKit.Core.Collections.Immutable"
      cw.write nil
      cw.write_block "namespace #{models_namespace}" do
        cw.write resolve_api_model(model_name, model).struct
      end
      output.write cw.text
    end
  end

  requests = data["paths"].map {|request_path, request| resolve_api_request(data["basePath"] + request_path, request["get"])}.compact

  File.open("#{name}ApiConnection.Generated.cs", "w") do |output|
    cw = CodeWriter.new
    cw.write_using_namespace models_namespace
    cw.write_using_namespace "PatchKit.Core.Collections.Immutable"
    cw.write_using_namespace "PatchKit.Core"
    cw.write_using_namespace "Newtonsoft.Json"
    cw.write_using_namespace "System.Collections.Generic"
    cw.write_using_namespace "System"
    cw.write nil
    cw.write_block "namespace PatchKit.Api" do
      cw.write_block "public partial class #{name}ApiConnection" do
        cw.write requests.map {|r| r.class_method}.join("\n\n")
      end
    end
    output.write cw.text
  end

  File.open("I#{name}ApiConnection.Generated.cs", "w") do |output|
    cw = CodeWriter.new
    cw.write_using_namespace models_namespace
    cw.write_using_namespace "PatchKit.Core.Collections.Immutable"
    cw.write_using_namespace "PatchKit.Core"
    cw.write nil
    cw.write_block "namespace PatchKit.Api" do
      cw.write_block "public partial interface I#{name}ApiConnection" do
        cw.write requests.map {|r| r.interface_method}.join("\n\n")
      end
    end
    output.write cw.text
  end
end
 
generate nil
generate "Keys"
