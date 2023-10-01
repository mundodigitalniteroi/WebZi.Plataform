﻿namespace WebZi.Plataform.CrossCutting.Web
{
    /// <summary>
    /// HTTP response status codes indicate whether a specific HTTP request has been successfully completed.
    /// </summary>
    public enum HtmlStatusCodeEnum
    {
        /// <summary>
        /// The request succeeded. The result meaning of "success" depends on the HTTP method:
        /// GET: The resource has been fetched and transmitted in the message body.
        /// HEAD: The representation headers are included in the response without any message body.
        /// PUT or POST: The resource describing the result of the action is transmitted in the message body.
        /// TRACE: The message body contains the request message as received by the server.
        /// </summary>
        Ok = 200,

        /// <summary>
        /// The server cannot or will not process the request due to something that is perceived to be a client error (e.g., malformed request syntax, invalid request message framing, or deceptive request routing).
        /// </summary>
        BadRequest = 400,

        /// <summary>
        /// Although the HTTP standard specifies "unauthorized", semantically this response means "unauthenticated". That is, the client must authenticate itself to get the requested response.
        /// </summary>
        Unauthorized = 401,

        /// <summary>
        /// The client does not have access rights to the content; that is, it is unauthorized, so the server is refusing to give the requested resource. Unlike 401 Unauthorized, the client's identity is known to the server.
        /// </summary>
        Forbidden = 403,

        /// <summary>
        /// The server cannot find the requested resource. In the browser, this means the URL is not recognized. In an API, this can also mean that the endpoint is valid but the resource itself does not exist. Servers may also send this response instead of 403 Forbidden to hide the existence of a resource from an unauthorized client. This response code is probably the most well known due to its frequent occurrence on the web.
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// The server has encountered a situation it does not know how to handle.
        /// </summary>
        InternalServerError = 500
    }
}