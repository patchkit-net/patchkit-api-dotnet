require_relative 'api_request.rb'
require_relative 'api_request_parameter_resolver.rb'
require_relative 'api_type_resolver.rb'

def resolve_api_request(request_path, request)
  return nil if request.nil?
  return nil if request_path.start_with? "/1/system"

  params = request["parameters"]
  params = [] if params.nil?

  ApiRequest.new request["summary"], 
    request_path,
    resolve_api_type(request["responses"]["200"]["schema"]), 
    params.map {|e| resolve_api_request_parameter(e)},
    request["description"],
    request["deprecated"]
end