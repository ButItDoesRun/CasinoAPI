using DataLayer.DataTransferModel;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace DataLayer;

public static class Util
{

    public static string RemoveSpaces(this string data)
    {
        return string.Concat(data.Where(c => !char.IsWhiteSpace(c)));
    }

    public static bool IsNullOrEmpty(this string? data)
    {
        return string.IsNullOrEmpty(data);
    }
}