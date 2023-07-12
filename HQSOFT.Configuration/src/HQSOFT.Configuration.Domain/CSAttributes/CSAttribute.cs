using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace HQSOFT.Configuration.CSAttributes
{
    public class CSAttribute : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string AttributeID { get; set; }

        [NotNull]
        public virtual string Description { get; set; }

        public virtual ControlType ControlType { get; set; }

        [CanBeNull]
        public virtual string? EntryMask { get; set; }

        [CanBeNull]
        public virtual string? RegExp { get; set; }

        [CanBeNull]
        public virtual string? List { get; set; }

        public virtual bool IsInternal { get; set; }

        public virtual bool ContainsPersonalData { get; set; }

        [CanBeNull]
        public virtual string? ObjectName { get; set; }

        [CanBeNull]
        public virtual string? FieldName { get; set; }

        public CSAttribute()
        {

        }

        public CSAttribute(Guid id, string attributeID, string description, ControlType controlType, string entryMask, string regExp, string list, bool isInternal, bool containsPersonalData, string objectName, string fieldName)
        {

            Id = id;
            Check.NotNull(attributeID, nameof(attributeID));
            Check.Length(attributeID, nameof(attributeID), CSAttributeConsts.AttributeIDMaxLength, 0);
            Check.NotNull(description, nameof(description));
            Check.Length(description, nameof(description), CSAttributeConsts.DescriptionMaxLength, 0);
            Check.Length(entryMask, nameof(entryMask), CSAttributeConsts.EntryMaskMaxLength, 0);
            Check.Length(regExp, nameof(regExp), CSAttributeConsts.RegExpMaxLength, 0);
            Check.Length(objectName, nameof(objectName), CSAttributeConsts.ObjectNameMaxLength, 0);
            Check.Length(fieldName, nameof(fieldName), CSAttributeConsts.FieldNameMaxLength, 0);
            AttributeID = attributeID;
            Description = description;
            ControlType = controlType;
            EntryMask = entryMask;
            RegExp = regExp;
            List = list;
            IsInternal = isInternal;
            ContainsPersonalData = containsPersonalData;
            ObjectName = objectName;
            FieldName = fieldName;
        }

    }
}