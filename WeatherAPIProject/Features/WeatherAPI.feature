Feature: WeatherAPI
	
@happypath
Scenario: A happy holidaymaker
     Given I like to holiday in Sydney
     | APIKey                           | ID     | mode | units  |
	 | 0332cddd07506bf8efbb903c91a89530 | 524901 | json | metric |
	 And I only like to holiday on Thursdays
     When I look up the weather forecast
	 Then I receive the weather forecast
	 And the temperature is warmer than 10.00 degree.
	
@negative
Scenario: A happy holidaymaker invalid access
     Given I like to holiday in Sydney
	 | APIKey | ID     | mode | units  |
	 | 11111  | 524901 | json | metric |
     And I only like to holiday on Thursdays
     When I look up the weather forecast with validation

@Additional
Scenario: A happy holidaymaker validate xml response
     Given I like to holiday in Sydney
	 | APIKey                           | ID     | mode | units  |
	 | 0332cddd07506bf8efbb903c91a89530 | 524901 | xml  | metric |
     And I only like to holiday on Thursdays
     When I look up the weather forecast response type
	 Then I receive the weather forecast Response

Scenario: A happy holidaymaker validate html response
     Given I like to holiday in Sydney
	 | APIKey                           | ID     | mode | units  |
	 | 0332cddd07506bf8efbb903c91a89530 | 524901 | html  | metric |
     And I only like to holiday on Thursdays
     When I look up the weather forecast response type
	 Then I receive the weather forecast Response


Scenario: A happy holidaymaker validate csv response
     Given I like to holiday in Sydney
	 | APIKey                           | ID     | mode | units  |
	 | 0332cddd07506bf8efbb903c91a89530 | 524901 | csv  | metric |
     And I only like to holiday on Thursdays
     When I look up the weather forecast response type
	 Then I receive the weather forecast Response	 
