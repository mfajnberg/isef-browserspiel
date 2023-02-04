﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json.Serialization;
using web_api.GameModel.Sites;
using web_api.GameModel.Worldmap;

namespace web_api.DTOs
{
    /// <summary>
    ///  Data Transfer Object for HexTiles
    /// </summary>
    [DebuggerDisplay("HexTile: Q={AxialCoordinates.Q}, R={AxialCoordinates.R}, Site={SiteType}")]
    public class HexTileDTO
    {
        public HexVector AxialCoordinates { get; set; }

        public SiteType SiteType { get; set; }
    }
}
