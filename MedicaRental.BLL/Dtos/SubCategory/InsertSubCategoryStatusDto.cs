﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicaRental.BLL.Dtos;
public record InsertSubCategoryStatusDto(bool isCreated, int? Id, string StatusMessage);

