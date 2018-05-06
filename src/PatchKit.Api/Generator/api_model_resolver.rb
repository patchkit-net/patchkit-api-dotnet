require_relative 'api_field_resolver.rb'
require_relative 'api_model.rb'

def resolve_api_model(model_name, model)
  fields = model["properties"].map {|fn, f| resolve_api_field(fn, f)}
  ApiModel.new model_name, fields
end