require_relative 'text_formatting.rb'
require_relative 'code_writer.rb'

class ApiRequest
  attr_reader :name, :parameters, :path
  def initialize(name, path, type, parameters, description, deprecated)
    @name = name
    @path = path
    @type = type
    @parameters = parameters
    @description = description
    @deprecated = deprecated
  end

  def interface_method
    cw = CodeWriter.new
    cw.write method_docs
    cw.write "#{method_definition};"
    cw.text
  end

  def class_method
    cw = CodeWriter.new
    cw.write_block "public #{method_definition}" do
      @parameters.map {|p| p.param_validator}.each do |v|
        cw.write v unless v.nil?
      end
      cw.write "timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));"
      cw.write "string path = \"#{@path}\";"
      cw.write "string query = string.Empty;"
      @parameters.each do |p|
        cw.write p.param_setter
      end
      cw.write "var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout);"
      cw.write method_response_deserialization
    end
    cw.text
  end

private
  def method_docs
    cw = CodeWriter.new
    cw.write_docs_summary "#{@description}"
    parameters.each do |p|
      cw.write p.param_docs
    end
    cw.write_docs_param "timeout", "Request timeout. If <c>null</c> then timeout is disabled"
    cw.text
  end

  def method_definition
    params = parameters.map {|p| p.param} + ["Timeout? timeout"]

    name = upper_camel_case(@name)
        .gsub(/Gets/, "Get")
        .gsub(/Lists/, "List")

    "#{@type.fullname} #{name}(#{params.join(', ')})"
  end

  def method_response_deserialization
    json_converter = @type.json_converter

    if json_converter.nil?
      "return JsonConvert.DeserializeObject<#{@type.fullname}>(response.Body);"
    else
      "return JsonConvert.DeserializeObject<#{@type.fullname}>(response.Body, new #{json_converter}());"
    end
  end
end