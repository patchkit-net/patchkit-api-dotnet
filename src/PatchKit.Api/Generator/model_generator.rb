require_relative "base_generator.rb"
require_relative "api_model_resolver.rb"
require_relative "text_formatting.rb"

class ModelGenerator < BaseGenerator
  attr_reader :output

  def initialize(api_name, name, data)
    super()
    @api_name = api_name
    @name = name
    @data = data
    generate
  end

private
  def generate
    @output = ""

    return if @data["type"] != "object"

    write_using_namespace "Newtonsoft.Json"
    write_using_namespace "PatchKit.Core.Collections.Immutable"
    write nil
    write_block "namespace #{models_namespace(@api_name)}" do
      write resolve_api_model(@name, @data).struct
    end
  end
end