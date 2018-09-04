require 'json'
require_relative 'text_formatting.rb'

class CodeWriter
  attr_reader :text

  def initialize
    @indent_level = 0
    @text = ""
  end

  def write(text)
    indent = "    " * @indent_level
    indent_text = text.gsub("\n", "\n#{indent}") unless text.nil?
    if !@text.empty?
      @text += "\n"
    end
    @text += "#{indent}#{indent_text}"
  end

  def write_comment(text)
    indent = "    " * @indent_level + "/// "
    indent_text = text.gsub("\n", "\n#{indent}") unless text.nil?
    if !@text.empty?
      @text += "\n"
    end
    @text += "#{indent}#{indent_text}"
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

  def write_docs_summary(summary)
    write_comment "<summary>"
    write_comment summary
    write_comment "</summary>"
  end

  def write_docs_param(name, description)
    write_comment "<param name=\"#{name}\">#{description}</param>"
  end

  def write_using_namespace(ns)
    write "using #{ns};"
  end
end
