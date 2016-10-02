using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Узагальнені класи для обгортання результатів які повертатимуть деякі методи
/// </summary>
namespace MusicTesting.Models.HelperModel
{
    public class Result<T>
    {
        public Object Object { get; set; }
        public bool IsSucces { get; set; }
        public string Message { get; set; }
    }

    public class Result
    {
        public Object Object { get; set; }
        public bool IsSucces { get; set; }
        public string Message { get; set; }
    }
}