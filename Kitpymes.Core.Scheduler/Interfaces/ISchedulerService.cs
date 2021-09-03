// -----------------------------------------------------------------------
// <copyright file="IWorkerService.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Scheduler
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public interface ISchedulerService
    {
        IWorker GetWorker(string workerId);

        /// <summary>
        /// Registra manualmente una clase que implementa IAgentWorker. 
        /// Cada vez que el framework necesite depositar trabajos en el IAgentWorker se creará una instancia nueva.
        /// Esta sobrecarga especifica un identificador de worker.
        /// </summary>
        /// <param name="worker">Identificador del agent worker. Sobreescribe el workerID especificado por el atributo 'WorkerID' si éste existiese.</param>
        void RegisterWorker(Type worker);

        IEnumerable<Type> DiscoverWorkers(Assembly assembly);
    }
}
