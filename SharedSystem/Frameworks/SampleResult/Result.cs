using FluentResults;

namespace SampleResult;

public class Result : object
{
	public Result() : base()
	{
		_errors =
				new System.Collections.Generic.List<string>();

		_successes =
				new System.Collections.Generic.List<string>();
	}

	public bool IsFailed { get; set; }

	public bool IsSuccess { get; set; }

	[System.Text.Json.Serialization.JsonIgnore]
	private System.Collections.Generic.List<string> _errors;

	public System.Collections.Generic.List<string> Errors
	{
		get
		{
			return _errors;
		}
		set
		{
			_errors = value;
		}
	}

	[System.Text.Json.Serialization.JsonIgnore]
	private System.Collections.Generic.List<string> _successes;

	public System.Collections.Generic.List<string> Successes
	{
		get
		{
			return _successes;
		}
		set
		{
			_successes = value;
		}
	}

	public void AddErrorMessage(string? message)
	{
		message =
				String.Fix(text: message);

		if (message == null)
		{
			return;
		}

		if (_errors.Contains(message))
		{
			return;
		}

		_errors.Add(message);
	}

	public void RemoveErrorMessage(string? message)
	{
		message =
				String.Fix(text: message);

		if (message == null)
		{
			return;
		}

		_errors.Remove(message);
	}

	public void ClearErrorMessages()
	{
		_errors.Clear();
	}

	public void AddSuccessMessage(string? message)
	{
		message =
				String.Fix(text: message);

		if (message == null)
		{
			return;
		}

		if (_successes.Contains(message))
		{
			return;
		}

		_successes.Add(message);
	}

	public void RemoveSuccessMessage(string? message)
	{
		message =
				String.Fix(text: message);

		if (message == null)
		{
			return;
		}

		_successes.Remove(message);
	}

	public void ClearSuccessMessages()
	{
		_successes.Clear();
	}
}

public class Result<T> : Result
{
	public Result() : base()
	{
	}

	public T? Value { get; set; }
}

public static class ResultExtensions
{
	static ResultExtensions()
	{
	}

	public static Result ConvertToSampleResult(this FluentResults.Result fluentResult)
	{
		Result result = new()
		{
			IsFailed = fluentResult.IsFailed,
			IsSuccess = fluentResult.IsSuccess,
		};

		if (fluentResult.Errors != null)
		{
			foreach (IError item in fluentResult.Errors)
			{
				result.AddErrorMessage(message: item.Message);
			}
		}

		if (fluentResult.Successes != null)
		{
			foreach (ISuccess item in fluentResult.Successes)
			{
				result.AddSuccessMessage(message: item.Message);
			}
		}

		return result;
	}

	public static Result<T> ConvertToSampleResult<T>(this FluentResults.Result<T> fluentResult)
	{
		Result<T> result = new()
		{
			IsFailed = fluentResult.IsFailed,
			IsSuccess = fluentResult.IsSuccess,
		};

		if (fluentResult.IsFailed == false)
		{
			result.Value = fluentResult.Value;
		}

		if (fluentResult.Errors != null)
		{
			foreach (IError item in fluentResult.Errors)
			{
				result.AddErrorMessage(message: item.Message);
			}
		}

		if (fluentResult.Successes != null)
		{
			foreach (ISuccess item in fluentResult.Successes)
			{
				result.AddSuccessMessage(message: item.Message);
			}
		}

		return result;
	}
}