using SampleResult;
using Microsoft.AspNetCore.Mvc;

namespace InfrastructureSeedworks.ActionFilter;

public class ResultActionFilter
{
    // overload
    // **************************************************
    [NonAction]
    protected IActionResult FluentResult<TResult>(FluentResults.Result<TResult> result)
    {
        Result<TResult> res =
            result.ConvertToSampleResult();

        if (res.IsSuccess)
        {
            return new OkObjectResult(res);
        }
        else
        {
            return new BadRequestObjectResult(res);
        }
    }
    // **************************************************

    // **************************************************
    [NonAction]
    protected IActionResult FluentResult(FluentResults.Result result)
    {
        Result res =
            result.ConvertToSampleResult();

        if (res.IsSuccess)
        {
            return new OkObjectResult(res);
        }
        else
        {
            return new BadRequestObjectResult(res);
        }
    }
    // **************************************************
}