Feature: Login Feature
  As a user
  I want to login to the Mars portal using credentials from JSON
  So that I can access my dashboard

  Scenario: Successful login using JSON credentials
    Given I navigate to the Mars portal login page
    When I enter valid username and password
    And I click on the login button
    Then I should be redirected to the dashboard
    And the user name should be displayed on the top right corner

