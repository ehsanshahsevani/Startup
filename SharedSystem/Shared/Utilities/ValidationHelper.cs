﻿namespace Utilities;

public static class ValidationHelper
{
	static ValidationHelper()
	{
	}

	public static bool IsValid(object entity)
	{
		var validationContext =
			new System.ComponentModel.DataAnnotations.ValidationContext(instance: entity);

		var validationResults =
			new List<System.ComponentModel.DataAnnotations.ValidationResult>();

		var isValid =
			System.ComponentModel.DataAnnotations.Validator
			.TryValidateObject(instance: entity, validationContext: validationContext,
			validationResults: validationResults, validateAllProperties: true);

		return isValid;
	}

	public static IList<System.ComponentModel.DataAnnotations.ValidationResult> GetValidationResults(object entity)
	{
		var validationContext =
			new System.ComponentModel.DataAnnotations.ValidationContext(instance: entity);

		var validationResults =
			new List<System.ComponentModel.DataAnnotations.ValidationResult>();

		System.ComponentModel.DataAnnotations.Validator
			.TryValidateObject(instance: entity, validationContext: validationContext,
			validationResults: validationResults, validateAllProperties: true);

		return validationResults;
	}
}