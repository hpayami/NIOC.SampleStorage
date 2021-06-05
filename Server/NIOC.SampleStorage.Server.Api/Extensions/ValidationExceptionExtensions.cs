﻿using Bit.Core.Implementations;
using NIOC.SampleStorage.Shared.Core.Exceptions;
using NIOC.SampleStorage.Shared.Core.POCOs;
using System;
using System.Collections.Generic;

namespace NIOC.SampleStorage.Server.Api.Extensions
{
    public static class ValidationExceptionExtensions
    {
        public static string GetModelErrorWrapper(this Exception exception)
        {
            ModelErrorWrapper? errorWrapper;

            if (exception is ValidationException validationException)
                errorWrapper = validationException.Error;
            else
            {
                errorWrapper = new ModelErrorWrapper
                {
                    Errors = new List<ATAModelError>
                    {
                        new ATAModelError
                        {
                            Messages = new List<string>
                            {
                                exception.Message
                            },
                            Exceptions = new List<Exception>
                            {
                                exception
                            }
                        }
                    }
                };
            }

            return DefaultJsonContentFormatter.Current.Serialize(errorWrapper);
        }
    }
}