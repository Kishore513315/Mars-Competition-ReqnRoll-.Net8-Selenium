Feature: Certification Management
  As a logged-in user
  I want to manage certification records
  So that I can add, edit, validate, and delete certifications

 Background:
  Given I navigate to the Mars portal login page
  When I enter valid username and password
  And I click on the login button
  Then I should be redirected to the dashboard
  And the user navigates to the Profile page
  And the user opens the Certifications section


  @Order(1)
  @TC_01 @Create
  Scenario: TC_01 - Create new certification
    When the user creates a certification using test data "TC_01"
    Then a success notification should be displayed for certification "TC_01" with action "added"

@Order(2)
  @TC_02 @Create @Duplicate
  Scenario: TC_02 - Create duplicate certification
    Given the certification record exists using test data "TC_01"
    When the user creates a certification using test data "TC_02"
    Then an error notification should be displayed with message key "ALREADY_EXISTS_CERTIFICATION_ERROR"

@Order(3)
@TC_03 @Create @Duplicate
  Scenario: TC_03 - Create certification with same name and from but different year
    When the user creates a certification using test data "TC_03"
    Then an error notification should be displayed with message key "DUPLICATE_CERTIFICATION_ERROR"

@Order(4)
  @TC_04 @Edit
  Scenario: TC_04 - Edit existing certification name
    Given the certification record exists using test data "TC_01"
    When the user updates the certification name using test data "TC_04"
    Then a success notification should be displayed for certification "TC_04" with action "updated"

@Order(5)
  @TC_05 @Create
  Scenario: TC_05 - Create certification using numeric values
    When the user creates a certification using test data "TC_05"
    Then a success notification should be displayed for certification "TC_05" with action "added"

@Order(6)
  @TC_06 @Delete
  Scenario: TC_06 - Delete certification created with numeric values
    Given the certification record exists using test data "TC_05"
    When the user deletes the certification using test data "TC_05"
    Then a success notification should be displayed for certification "TC_05" with action "deleted"

@Order(7)
  @TC_07 @Create
  Scenario: TC_07 - Create certification with special characters
    When the user creates a certification using test data "TC_07"
    Then a success notification should be displayed for certification "TC_07" with action "added"

@Order(8)
  @TC_08 @Boundary
  Scenario: TC_08 - Create certification with very long text values
    When the user creates a certification using test data "TC_08"
    Then a success notification should be displayed for certification "TC_08" with action "added"

@Order(9)
  @TC_010 @Validation @Negative
  Scenario: TC_010 - Empty field validation during certification creation
    When the user attempts to create a certification using test data "TC_010"
    Then an error notification should be displayed with message key "MANDATORY_FIELDS_ERROR"

@Order(10)
  @TC_011 @Cleanup
  Scenario: TC_011 - Cleanup all certifications created during test execution
    When the user deletes all certifications created during this test run
    Then no test certification records should exist

