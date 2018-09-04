require_relative 'code_writer.rb'

class ApiModel
  def initialize(name, fields)
    @name = name
    @fields = fields
  end

  def file(api_name)
    cw = CodeWriter.new
    write_using_namespace "Newtonsoft.Json"
    write_using_namespace "PatchKit.Core.Collections.Immutable"
    write nil
    write_block "namespace #{models_namespace(@api_name)}" do
      write resolve_api_model(@name, @data).struct
    end
    cw.text
  end

  def struct
    cw = CodeWriter.new
    cw.write_block "public struct #{@name}" do
      cw.write struct_constructor
      cw.write nil
      cw.write struct_fields
    end
    cw.text
  end

private
  def struct_fields
    @fields.map {|f| f.field}.join("\n\n")
  end

  def struct_constructor
    cw = CodeWriter.new

    parameters = @fields.map {|f| f.param}.join(', ')

    cw.write "[JsonConstructor]"
    cw.write_block "public #{@name}(#{parameters})" do
      @fields.each do |f|
        cw.write "#{f.field_name} = #{f.param_name};"
      end
    end

    cw.text
  end
end