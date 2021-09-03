// -----------------------------------------------------------------------
// <copyright file="ErrorMsg.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Scheduler
{
    using System;

    public static class ErrorMsg
    {
        public static string AttributedWorker(string workerId)
        => $"El tipo especificado contiene un attributo 'WorkerID' y no puede especificarse otro en el registro del worker: '{workerId}'.";

        public static string DuplicatedWorkerId(string workerId)
        => $"No puede haber más de una implementación de IWorker con el mismo identificador: '{workerId}'.";

        public static string ExecuteWorker(string workerId)
        => $"Error al ejecutar el Worker '{workerId}'.";

        public static string InvalidKeyDictionary(string key)
        => $"La clave '{key}' no existe en la lista de workers disponibles.";

        public static string NotImplementIWorker(Type type)
        => $"El tipo '{type.Name}' no implementa IWorker y no puede ser registrado.";

        public static string InvalidDeserialized(Type? type = null)
        {
            if (type is null)
            {
                return $"Error al deserializar un objeto de tipo dinámico.";
            }

            return $"Error al deserializar un objeto de tipo '{type.Name}'.";
        }

        public static string WorkerIdNotFound(string? workerId = null)
        {
            if (string.IsNullOrWhiteSpace(workerId))
            {
                return "El WorkerId solicitado no se encontrada.";
            }

            return $"El WorkerId '{workerId}' no fue encontrado.";
        }
    }
}
