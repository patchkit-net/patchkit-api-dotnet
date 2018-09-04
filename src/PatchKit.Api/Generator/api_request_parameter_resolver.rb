require_relative 'api_request_parameter.rb'
require_relative 'api_type_resolver.rb'

def resolve_api_request_parameter(parameter)
  ApiRequestParameter.new parameter["name"], resolve_api_type(parameter), parameter["description"], parameter["in"], parameter["required"]
end