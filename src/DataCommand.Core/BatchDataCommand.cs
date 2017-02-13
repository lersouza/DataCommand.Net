using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DataCommand.Core
{
    /// <summary>
    /// Represents a data command for performing batch operations.
    /// </summary>
    public abstract class BatchDataCommand<TEntity, TReturn> : DataCommand<TReturn>
    {
        /// <summary>
        /// Initializes a new instace of <see cref="BatchDataCommand{T}"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="batchSize">The size for the batch operations (i.e., the size of objects to be hold before an execution happens)</param>
        /// <param name="options"></param>
        /// <param name="loggerFactory">The Factory Service to be used when creating loggers for this command.</param>
        public BatchDataCommand(string name, int batchSize, DataCommandOptions options, ILoggerFactory loggerFactory)
            : base(name, options, loggerFactory)
        {
            BatchSize = batchSize;
        }

        /// <summary>
        /// Gets the size of this batch command.
        /// </summary>
        public int BatchSize { get; private set; }

        /// <summary>
        /// Gets the list of entities kept in memory before an execution
        /// </summary>
        protected IList<TEntity> Entities { get; private set; } = new List<TEntity>();

        /// <summary>
        /// Adds a new entity to this batch command.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This method blocks the current thread when <see cref="BatchSize"/> is reached. 
        ///         At this time, it flushes the data executing the Run Command.
        ///     </para>
        ///     <para>
        ///         When execution the query, subclasses must look into the <see cref="Entities"/> property to get the collection of entities to be flushed.
        ///     </para>
        /// </remarks>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void AddEntity(TEntity entity)
        {
            Entities.Add(entity);

            //Trigger event
            OnEntityAdded(entity);

            if (Entities.Count >= BatchSize)
            {
                //Runs this command
                Run();

                //Empties the entities list
                Entities.Clear();

                //Trigger the event
                OnEntitiesClear();
            }
        }

        /// <summary>
        /// Executed whenever a new entity is successfully added to this command.
        /// </summary>
        /// <remarks>
        ///     <para>This method is useful when actions should be performed on a entity basis.</para>
        /// </remarks>
        /// <param name="entity">The just added entity.</param>
        protected virtual void OnEntityAdded(TEntity entity)
        {
        }

        /// <summary>
        /// Executed whenever a the entities list <see cref="IList{T}.Clear"/> method is called.
        /// </summary>
        /// <remarks>
        ///     <para>This method is useful when actions should be performed for disposing resources.</para>
        /// </remarks>
        protected virtual void OnEntitiesClear()
        {
        }
    }
}
