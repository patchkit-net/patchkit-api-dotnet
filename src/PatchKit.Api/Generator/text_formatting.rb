def camel_case(text)
  text.gsub(/[\s,_](.)/) {|e| $1.upcase}
end

def upper_camel_case(text)
  output = camel_case(text)
  output[0, 1].upcase + output[1..-1]
end

def lower_camel_case(text)
  output = camel_case(text)
  output[0, 1].downcase + output[1..-1]
end