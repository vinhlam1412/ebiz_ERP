using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace HQSOFT.Configuration.CSAttributes
{
    public class CSAttributeManager : DomainService
    {
        private readonly ICSAttributeRepository _cSAttributeRepository;

        public CSAttributeManager(ICSAttributeRepository cSAttributeRepository)
        {
            _cSAttributeRepository = cSAttributeRepository;
        }

        public async Task<CSAttribute> CreateAsync(
        string attributeID, string description, ControlType controlType, string entryMask, string regExp, string list, bool isInternal, bool containsPersonalData, string objectName, string fieldName)
        {
            Check.NotNullOrWhiteSpace(attributeID, nameof(attributeID));
            Check.Length(attributeID, nameof(attributeID), CSAttributeConsts.AttributeIDMaxLength);
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.Length(description, nameof(description), CSAttributeConsts.DescriptionMaxLength);
            Check.NotNull(controlType, nameof(controlType));
            Check.Length(entryMask, nameof(entryMask), CSAttributeConsts.EntryMaskMaxLength);
            Check.Length(regExp, nameof(regExp), CSAttributeConsts.RegExpMaxLength);
            Check.Length(objectName, nameof(objectName), CSAttributeConsts.ObjectNameMaxLength);
            Check.Length(fieldName, nameof(fieldName), CSAttributeConsts.FieldNameMaxLength);

            var cSAttribute = new CSAttribute(
             GuidGenerator.Create(),
             attributeID, description, controlType, entryMask, regExp, list, isInternal, containsPersonalData, objectName, fieldName
             );

            return await _cSAttributeRepository.InsertAsync(cSAttribute);
        }

        public async Task<CSAttribute> UpdateAsync(
            Guid id,
            string attributeID, string description, ControlType controlType, string entryMask, string regExp, string list, bool isInternal, bool containsPersonalData, string objectName, string fieldName, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(attributeID, nameof(attributeID));
            Check.Length(attributeID, nameof(attributeID), CSAttributeConsts.AttributeIDMaxLength);
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.Length(description, nameof(description), CSAttributeConsts.DescriptionMaxLength);
            Check.NotNull(controlType, nameof(controlType));
            Check.Length(entryMask, nameof(entryMask), CSAttributeConsts.EntryMaskMaxLength);
            Check.Length(regExp, nameof(regExp), CSAttributeConsts.RegExpMaxLength);
            Check.Length(objectName, nameof(objectName), CSAttributeConsts.ObjectNameMaxLength);
            Check.Length(fieldName, nameof(fieldName), CSAttributeConsts.FieldNameMaxLength);

            var cSAttribute = await _cSAttributeRepository.GetAsync(id);

            cSAttribute.AttributeID = attributeID;
            cSAttribute.Description = description;
            cSAttribute.ControlType = controlType;
            cSAttribute.EntryMask = entryMask;
            cSAttribute.RegExp = regExp;
            cSAttribute.List = list;
            cSAttribute.IsInternal = isInternal;
            cSAttribute.ContainsPersonalData = containsPersonalData;
            cSAttribute.ObjectName = objectName;
            cSAttribute.FieldName = fieldName;

            cSAttribute.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _cSAttributeRepository.UpdateAsync(cSAttribute);
        }

    }
}