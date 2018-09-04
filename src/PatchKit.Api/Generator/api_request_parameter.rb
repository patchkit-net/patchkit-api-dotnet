require_relative 'text_formatting.rb'
require_relative 'code_writer.rb'

class ApiRequestParameter
  def initialize(name, type, description, kind, required)
    @name = name
    @type = type
    @description = description
    @kind = kind
    @required = required
  end

  def param
    "#{param_type} #{param_name}"
  end

  def param_type
    if param_nullable
      "#{@type.fullname}?"
    else
      @type.fullname
    end
  end

  def param_docs
    desc = @description
    desc = "" if desc.nil?
    desc += " (required)" if @required
    desc += " (optional)" unless @required

    cw = CodeWriter.new
    cw.write_docs_param param_name, desc
    cw.text
  end

  def param_validator
    if @required && @type.fullname == "string"
      cw = CodeWriter.new
      cw.write_block "if (#{param_name} == null)" do
        cw.write "throw new ArgumentNullException(nameof(#{param_name}));"
      end
      return cw.text
    else
      return nil
    end
  end

  def param_setter
    cw = CodeWriter.new

    case @kind
    when "query"
      if @required
        cw.write param_query_setter
      else
        cw.write_block "if (#{param_name} != null)" do
          cw.write param_query_setter
        end
      end
    when "path"
      cw.write param_path_setter
    else
      cw.write "//TODO: Unsupported parameter kind '#{@kind}'"
    end

    cw.text
  end

private
  def param_nullable
    if @required
      false
    else
      if @type.fullname == "string"
        false
      else
        true
      end
    end
  end

  def param_name
    lower_camel_case(@name)
  end

  def param_value
    if param_nullable
      "#{param_name}.Value"
    else
      param_name
    end
  end

  def param_query_setter
    "SetQueryParam(ref query, \"#{@name}\", #{param_value}.ToString());"
  end

  def param_path_setter
    "SetPathParam(ref path, \"#{@name}\", #{param_name}.ToString());"
  end
end