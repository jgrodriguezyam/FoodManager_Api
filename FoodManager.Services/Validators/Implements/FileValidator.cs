using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using FoodManager.Infrastructure.Files;
using FoodManager.Infrastructure.Files.ElementConfigs;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Validators;
using FoodManager.Services.Validators.Interfaces;
using ServiceStack.Common;
using ServiceStack.Common.Extensions;

namespace FoodManager.Services.Validators.Implements
{
    public class FileValidator : BaseValidator<File>, IFileValidator
    {
        public FileValidator()
        {
            RuleSet("Base", () => Custom(FileSignatureValidate));
        }

        public ValidationFailure FileSignatureValidate(File file, ValidationContext<File> context)
        {
            var validFiles = FileSettings.ValidFiles;
            var fileToValid = validFiles.FirstOrDefault(validFile => validFile.Name.Equals(file.Extension));
            if (fileToValid.IsNull())
                return new ValidationFailure("File", "No es un archivo valido");

            var signaturesToValid = fileToValid.Signatures.ToList<HexInstanceElement>();
            foreach (var hexInstanceElement in signaturesToValid)
            {
                var byteArrayValid = hexInstanceElement.Hex.Split(' ').Select(hex => Convert.ToByte(hex, 16)).ToArray();
                var byteArrayToValid = file.Stream.ToBytes().Take(byteArrayValid.Count());
                var isValid = byteArrayValid.Contains(byteArrayToValid.ToArray());
                if (isValid)
                    return null;
            }
            return new ValidationFailure("File", "No es un archivo valido");
        }
    }
}