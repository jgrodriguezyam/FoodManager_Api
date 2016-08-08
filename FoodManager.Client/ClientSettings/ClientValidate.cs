using System.Net;
using FoodManager.Client.Exceptions;
using FoodManager.Infrastructure.Exceptions;

namespace FoodManager.Client.ClientSettings
{
    public static class ClientValidate
    {
        public static void ThrowIfNotSuccess(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case 0:
                    throw new ServerNotFoundException();

                case HttpStatusCode.NotFound:
                    ExceptionExtensions.ThrowCustomException(HttpStatusCode.NotFound, "No se encontro el registro");
                    throw new NotFoundException();

                case HttpStatusCode.BadRequest:
                    ExceptionExtensions.ThrowCustomException(HttpStatusCode.BadRequest, "Peticion erronea");
                    throw new BadRequestException();

                case HttpStatusCode.Unauthorized:
                    ExceptionExtensions.ThrowCustomException(HttpStatusCode.Unauthorized, "No estas autorizado");
                    throw new UnauthorizedException();

                case HttpStatusCode.InternalServerError:
                    ExceptionExtensions.ThrowCustomException(HttpStatusCode.InternalServerError, "Error Interno");
                    throw new InternalServerException();

                case HttpStatusCode.MethodNotAllowed:
                    ExceptionExtensions.ThrowCustomException(HttpStatusCode.MethodNotAllowed, "Metodo no permitido");
                    throw new MethodNotAllowedException();

                case HttpStatusCode.NotAcceptable:
                    ExceptionExtensions.ThrowCustomException(HttpStatusCode.NotAcceptable, "Peticion no aceptada");
                    throw new NotFoundException();
            }
        }
    }
}