require_relative 'text_formatting.rb'

class ApiType
  def initialize(name, is_array)
    @name = name
    @is_array = is_array
  end

  def fullname
    if @is_array
      "ImmutableArray<#{@name}>"
    else
      @name
    end
  end

  def json_converter
    if @is_array
      "ImmutableArrayJsonConverter<#{@name}>"
    else
      nil
    end
  end
end