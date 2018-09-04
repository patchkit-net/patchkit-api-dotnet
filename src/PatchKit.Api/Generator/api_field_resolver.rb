require_relative 'api_type_resolver.rb'
require_relative 'api_field.rb'

def resolve_api_field(field_name, field)
  ApiField.new field_name, resolve_api_type(field), field["description"]
end