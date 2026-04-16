using TalentInsights.Application.Models.Responses;

namespace TalentInsights.Application.Helpers
{
    public static class ResponseHelper
    {
        public static GenericResponse<T> Create<T>(T data, int? count, List<string>? errors = null, string? message = null)
        {
            var response = new GenericResponse<T>
            {
                Data = data,
                Message = message ?? "Solicitud realizada correctamente",
                Count = count ?? 0,
                Errors = errors ?? []
            };

            return response;
        }
    }
}
