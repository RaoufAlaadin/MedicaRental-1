﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicaRental.BLL.Dtos.Brand;

public record  BrandDto(Guid Id,string Name,string CountryOfOrigin, byte[]? Image);