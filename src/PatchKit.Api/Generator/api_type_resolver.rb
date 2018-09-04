require_relative 'api_type.rb'

def resolve_array_api_type_name(type)
  resolve_api_type(type["items"]).fullname
end

def resolve_int_api_type_name(type)
  case type["format"]
  when "int64"
    "long"
  else
    "int"
  end
end

def resolve_number_api_type_name(type)
  case type['format']
  when 'float'
    'float'
  when 'double'
    'double'
  else
    raise "Cannot resolve number type of #{type}"
  end
end

def resolve_base_api_type(type)
  type_name = type["type"]
  case type_name
  when "string"
    ApiType.new "string", false
  when "boolean"
    ApiType.new "bool", false
  when "integer"
    ApiType.new resolve_int_api_type_name(type), false
  when "array"
    ApiType.new resolve_array_api_type_name(type), true
  when "number"
    ApiType.new resolve_number_api_type_name(type), false
  else
    raise "Cannot resolve base type of #{type}" if !type_name.start_with?("#/definitions/")
    return ApiType.new type_name.sub!("#/definitions/", ""), false
  end
end

def resolve_api_type(type)
  if type.key? "$ref"
    ApiType.new type["$ref"].gsub("#/definitions/", ""), false
  else
    resolve_base_api_type(type)
  end
end