using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utility.Results
{
	public class SuccessResult : Result
	{
		public SuccessResult(string message) : base(true, message)
		{
		}

		public SuccessResult() : base(true)
		{
		}
	}
}
