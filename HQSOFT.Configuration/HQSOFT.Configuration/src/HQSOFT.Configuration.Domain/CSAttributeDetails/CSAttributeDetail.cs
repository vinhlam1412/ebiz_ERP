using HQSOFT.Configuration.CSAttributes;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace HQSOFT.Configuration.CSAttributeDetails
{
    public class CSAttributeDetail : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string ValueID { get; set; }

        [NotNull]
        public virtual string Description { get; set; }

        public virtual uint? SortOrder { get; set; }

        public virtual bool Disabled { get; set; }
        public Guid? CSAttributeId { get; set; }

        public CSAttributeDetail()
        {

        }

        public CSAttributeDetail(Guid id, Guid? cSAttributeId, string valueID, string description, bool disabled, uint? sortOrder = null)
        {

            Id = id;
            Check.NotNull(valueID, nameof(valueID));
            Check.Length(valueID, nameof(valueID), CSAttributeDetailConsts.ValueIDMaxLength, 0);
            Check.NotNull(description, nameof(description));
            Check.Length(description, nameof(description), CSAttributeDetailConsts.DescriptionMaxLength, 0);
            ValueID = valueID;
            Description = description;
            Disabled = disabled;
            SortOrder = sortOrder;
            CSAttributeId = cSAttributeId;
        }

    }
}