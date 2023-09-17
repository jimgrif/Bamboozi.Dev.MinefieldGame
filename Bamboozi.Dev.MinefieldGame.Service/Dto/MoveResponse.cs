﻿using Bamboozi.Dev.MinefieldGame.StateRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bamboozi.Dev.MinefieldGame.Service.Dto
{
    public record MoveResponse
    (
        MoveType? MoveType,
        MoveOutcome? MoveOutcome,
        UserState UserState
    );
}
