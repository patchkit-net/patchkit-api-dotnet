require_relative 'text_formatting.rb'
require_relative 'code_writer.rb'

class ApiField
  def initialize(name, type, description)
    @name = name
    @type = type
    @description = description
  end

  def field_name
    upper_camel_case(@name)
  end

  def param_name
    lower_camel_case(@name)
  end

  def field
    cw = CodeWriter.new
    cw.write field_docs
    cw.write field_attributes
    cw.write "public #{@type.fullname} #{field_name} { get; }"
    cw.text
  end

  def param
    "#{@type.fullname} #{param_name}"
  end
  
private
  def field_attributes
    json_converter = @type.json_converter
    json_property = "JsonProperty(\"#{@name}\")"

    if json_converter.nil?
      "[#{json_property}]"
    else
      "[#{json_property}, JsonConverter(typeof(#{json_converter}))]"
    end
  end

  def field_docs
    cw = CodeWriter.new
    cw.write_docs_summary "#{@description}"
    cw.text
  end
end