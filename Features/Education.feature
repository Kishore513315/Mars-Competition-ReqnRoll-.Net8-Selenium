@login
Feature: Education Management
  As a logged-in user
  I want to manage my education details
  So that I can add, edit, validate, and delete education records successfully

  Background:
    Given the user is logged into the application
    And the user navigates to the Education section

  @Automated @AddEducation
  Scenario Outline: Add new education
    When the user adds a new education using test data "<TestCaseId>"
    Then the education record should be created successfully

    Examples:
      | TestCaseId |
      | TC-001 |
      | TC-002 |
      | TC-005 |
      | TC-006 |
      | TC-007 |

  @Automated @EditEducation
  Scenario Outline: Edit education record
    Given an education record exists using test data "TC-001"
    When the user edits the education record using test data "<TestCaseId>"
    Then the education record should be updated successfully

    Examples:
      | TestCaseId |
      | TC-003 |

  @Automated @DeleteEducation
  Scenario: Delete education record
    Given an education record exists using test data "TC-003"
    When the user deletes the education record
    Then the education record should be deleted successfully

  @Automated @MultipleRecords
  Scenario Outline: Add multiple education records
    When the user adds a new education using test data "<TestCaseId>"
    Then the education record should be created successfully

    Examples:
      | TestCaseId |
      | TC-008A |
      | TC-008B |

  @Automated @FieldValidation
  Scenario Outline: Validate mandatory field behavior
    When the user adds a new education using test data "<TestCaseId>"
    Then a validation message should be displayed
    And the education record should not be created

    Examples:
      | TestCaseId |
      | TC-009 |

  @Automated @Cleanup
  Scenario: Clean up all education records
    When the user deletes all education records created during the test session
    Then all test education records should be removed successfully





