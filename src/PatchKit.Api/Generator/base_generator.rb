require 'json'
require_relative 'text_formatting.rb'

class BaseGenerator
  def initialize
    @indent_level = 0
  end

protected
  def write(text)
    indent = "    " * @indent_level
    indent_text = text.gsub("\n", "\n#{indent}") unless text.nil?
    @output += "#{indent}#{indent_text}\n"
  end

  def write_comment(text)
    indent = "    " * @indent_level + "/// "
    indent_text = text.gsub("\n", "\n#{indent}") unless text.nil?
    @output += "#{indent}#{indent_text}\n"
  end

  def write_block(&block)
    write "{"
    @indent_level+=1
    block.call if block_given?
    @indent_level-=1
    write "}"
  end

  def write_block(text, &block)
    write text
    write "{"
    @indent_level+=1
    block.call if block_given?
    @indent_level-=1
    write "}"
  end

  def write_docs_param(parameter)
    write_comment "<param name=\"#{lower_camel_case(parameter["name"])}\">#{parameter["description"]}</param>"
  end

  def write_using_namespace(ns)
    write "using #{ns};"
  end

  def models_namespace(name)
    name.nil? ? "PatchKit.Api.Models" : "PatchKit.Api.Models.#{name}"
  end
end
