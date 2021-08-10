﻿using ASCOM;
using ASCOM.Standard.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ASCOM.Standard.AlpacaClients;

namespace ConformU
{
    public class TelescopeFacade : FacadeBaseClass, ITelescopeV3, IDisposable
    {

        // Create the test device in the facade base class
        public TelescopeFacade(Settings conformSettings, ConformLogger logger) : base(conformSettings, logger) { }

        #region Interface implementation

        public AlignmentMode AlignmentMode => (AlignmentMode)driver.AlignmentMode;

        public double Altitude => driver.Altitude;

        public double ApertureArea => driver.ApertureArea;

        public double ApertureDiameter => driver.ApertureDiameter;

        public bool AtHome => driver.AtHome;

        public bool AtPark => driver.AtPark;

        public double Azimuth => driver.Azimuth;

        public bool CanFindHome => driver.CanFindHome;

        public bool CanPark => driver.CanPark;

        public bool CanPulseGuide => driver.CanPulseGuide;

        public bool CanSetDeclinationRate => driver.CanSetDeclinationRate;

        public bool CanSetGuideRates => driver.CanSetGuideRates;

        public bool CanSetPark => driver.CanSetPark;

        public bool CanSetPierSide => driver.CanSetPierSide;

        public bool CanSetRightAscensionRate => driver.CanSetRightAscensionRate;

        public bool CanSetTracking => driver.CanSetTracking;

        public bool CanSlew => driver.CanSlew;

        public bool CanSlewAltAz => driver.CanSlewAltAz;

        public bool CanSlewAltAzAsync => driver.CanSlewAltAzAsync;

        public bool CanSlewAsync => driver.CanSlewAsync;

        public bool CanSync => driver.CanSync;

        public bool CanSyncAltAz => driver.CanSyncAltAz;

        public bool CanUnpark => driver.CanUnpark;

        public double Declination => driver.Declination;

        public double DeclinationRate { get => driver.DeclinationRate; set => driver.DeclinationRate = value; }
        public bool DoesRefraction { get => driver.DoesRefraction; set => driver.DoesRefraction = value; }

        public EquatorialCoordinateType EquatorialSystem => (EquatorialCoordinateType)driver.EquatorialSystem;

        public double FocalLength => driver.FocalLength;

        public double GuideRateDeclination { get => driver.GuideRateDeclination; set => driver.GuideRateDeclination = value; }
        public double GuideRateRightAscension { get => driver.GuideRateRightAscension; set => driver.GuideRateRightAscension = value; }

        public bool IsPulseGuiding => driver.IsPulseGuiding;

        public double RightAscension => driver.RightAscension;

        public double RightAscensionRate { get => driver.RightAscensionRate; set => driver.RightAscensionRate = value; }
        public PointingState SideOfPier { get => (PointingState)driver.SideOfPier; set => driver.SideOfPier = value; }

        public double SiderealTime => driver.SiderealTime;

        public double SiteElevation { get => driver.SiteElevation; set => driver.SiteElevation = value; }
        public double SiteLatitude { get => driver.SiteLatitude; set => driver.SiteLatitude = value; }
        public double SiteLongitude { get => driver.SiteLongitude; set => driver.SiteLongitude = value; }

        public bool Slewing => driver.Slewing;

        public short SlewSettleTime { get => driver.SlewSettleTime; set => driver.SlewSettleTime = value; }
        public double TargetDeclination { get => driver.TargetDeclination; set => driver.TargetDeclination = value; }
        public double TargetRightAscension { get => driver.TargetRightAscension; set => driver.TargetRightAscension = value; }
        public bool Tracking { get => driver.Tracking; set => driver.Tracking = value; }
        public DriveRate TrackingRate { get => (DriveRate)driver.TrackingRate; set => driver.TrackingRate = value; }

        public ITrackingRates TrackingRates
        {
            get
            {
                return new TrackingRatesFacade(driver);
            }
        }

        public DateTime UTCDate { get => driver.UTCDate; set => driver.UTCDate = value; }

        public void AbortSlew()
        {
            driver.AbortSlew();
        }

        public IAxisRates AxisRates(TelescopeAxis Axis)
        {
            return new AxisRatesFacade(Axis, driver, logger);
        }

        public bool CanMoveAxis(TelescopeAxis Axis)
        {
            return driver.CanMoveAxis(Axis);
        }

        public PointingState DestinationSideOfPier(double RightAscension, double Declination)
        {
            return (PointingState)driver.DestinationSideOfPier(RightAscension, Declination);
        }

        public void FindHome()
        {
            driver.FindHome();
        }

        public void MoveAxis(TelescopeAxis Axis, double Rate)
        {
            driver.MoveAxis(Axis, Rate);
        }

        public void Park()
        {
            driver.Park();
        }

        public void PulseGuide(GuideDirection Direction, int Duration)
        {
            driver.PulseGuide(Direction, Duration);
        }

        public void SetPark()
        {
            driver.SetPark();
        }

        public void SlewToAltAz(double Azimuth, double Altitude)
        {
            driver.SlewToAltAz(Azimuth, Altitude);
        }

        public void SlewToAltAzAsync(double Azimuth, double Altitude)
        {
            driver.SlewToAltAzAsync(Azimuth, Altitude);
        }

        public void SlewToCoordinates(double RightAscension, double Declination)
        {
            driver.SlewToCoordinates(RightAscension, Declination);
        }

        public void SlewToCoordinatesAsync(double RightAscension, double Declination)
        {
            driver.SlewToCoordinatesAsync(RightAscension, Declination);
        }

        public void SlewToTarget()
        {
            driver.SlewToTarget();
        }

        public void SlewToTargetAsync()
        {
            driver.SlewToTargetAsync();
        }

        public void SyncToAltAz(double Azimuth, double Altitude)
        {
            driver.SyncToAltAz(Azimuth, Altitude);
        }

        public void SyncToCoordinates(double RightAscension, double Declination)
        {
            driver.SyncToCoordinates(RightAscension, Declination);
        }

        public void SyncToTarget()
        {
            driver.SyncToTarget();
        }

        public void UnPark()
        {
            driver.UnPark();
        }

        #endregion
    }
}
