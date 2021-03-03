#pragma once
using namespace System;
#include "zoom_sdk_dotnet_wrap_def.h"
#include "video_setting_context_dotnet_wrap.h"
#include "zoom_sdk_dotnet_wrap_util.h"
namespace ZOOM_SDK_DOTNET_WRAP {
	public enum class SettingTabPage : int
	{
		SettingTabPage_General,
		SettingTabPage_Audio,
		SettingTabPage_Video,
	};

	public value class ShowSettingDlgParam
	{
	public:
		HWNDDotNet hParent;///< Parent window handle
		int top;///< setting dialog top position
		int left;///< setting dialog left position
		HWNDDotNet hSettingWnd;///< return setting dialog handle
		bool bShow;///< show or not
		SettingTabPage eTabPageType;
	};

	public interface class IGeneralSettingContextDotNetWrap
	{
	public:
		SDKError EnableDualScreenMode(bool bEnable);
		bool IsDualScreenModeEnabled();
		SDKError TurnOffAeroModeInSharing(bool bTurnoff);
		bool IsAeroModeInSharingTurnOff();
		SDKError EnableSplitScreenMode(bool bEnable);
		bool IsSplitScreenModeEnabled();
		SDKError EnableAutoFullScreenVideoWhenJoinMeeting(bool bEnable);
		bool IsAutoFullScreenVideoWhenJoinMeetingEnabled();
	};

	private ref class CGeneralSettingContextDotNetWrap sealed : public IGeneralSettingContextDotNetWrap
	{
	public:
		static property CGeneralSettingContextDotNetWrap^ Instance
		{
			CGeneralSettingContextDotNetWrap^ get() { return m_Instance; }
		}

		virtual SDKError EnableDualScreenMode(bool bEnable);
		virtual bool IsDualScreenModeEnabled();
		virtual SDKError TurnOffAeroModeInSharing(bool bTurnoff);
		virtual bool IsAeroModeInSharingTurnOff();
		virtual SDKError EnableSplitScreenMode(bool bEnable);
		virtual bool IsSplitScreenModeEnabled();
		virtual SDKError EnableAutoFullScreenVideoWhenJoinMeeting(bool bEnable);
		virtual bool IsAutoFullScreenVideoWhenJoinMeetingEnabled();
	private:
		CGeneralSettingContextDotNetWrap() {}
		virtual ~CGeneralSettingContextDotNetWrap() {}
		static CGeneralSettingContextDotNetWrap^ m_Instance = gcnew CGeneralSettingContextDotNetWrap;
	};

	public interface class IMicInfoDotNetWrap
	{
	public:
		String^ GetDeviceId();
		String^ GetDeviceName();
		bool IsSelectedDevice();
	};

	public interface class ISpeakerInfoDotNetWrap
	{
	public:
		String^ GetDeviceId();
		String^ GetDeviceName();
		bool IsSelectedDevice();
	};

	public delegate void onComputerMicDeviceChanged(array<IMicInfoDotNetWrap^>^ newMics);
	public delegate void onComputerSpeakerDeviceChanged(array<ISpeakerInfoDotNetWrap^>^ newSpeakers);

	public interface class IAudioSettingContextDotNetWrap
	{
	public:
		array<IMicInfoDotNetWrap^ >^ GetMicList();
		SDKError SelectMic(String^ deviceId, String^ deviceName);
		array<ISpeakerInfoDotNetWrap^ >^ GetSpeakerList();
		SDKError SelectSpeaker(String^ deviceId, String^ deviceName);
		SDKError EnableAutoJoinAudio(bool bEnable);
		bool IsAutoJoinAudioEnabled();
		SDKError EnableAutoAdjustMic(bool bEnable);
		bool IsAutoAdjustMicEnabled();
		SDKError EnableStereoAudio(bool bEnable);
		bool IsStereoAudioEnable();
		SDKError EnableMicOriginalInput(bool bEnable);
		bool IsMicOriginalInputEnable();
		SDKError EnableHoldSpaceKeyToSpeak(bool bEnable);
		bool IsHoldSpaceKeyToSpeakEnabled();
		SDKError EnableAlwaysMuteMicWhenJoinVoip(bool bEnable);
		bool IsAlwaysMuteMicWhenJoinVoipEnabled();
//		ITestAudioDeviceHelper* GetTestAudioDeviceHelper();
		SDKError SetMicVol(float& value);
		SDKError GetMicVol(float& value);
		SDKError SetSpeakerVol(float& value);
		SDKError GetSpeakerVol(float& value);
		void Add_CB_onComputerMicDeviceChanged(onComputerMicDeviceChanged^ cb);
		void Remove_CB_onComputerMicDeviceChanged(onComputerMicDeviceChanged^ cb);
		void Add_CB_onComputerSpeakerDeviceChanged(onComputerSpeakerDeviceChanged^ cb);
		void Remove_CB_onComputerSpeakerDeviceChanged(onComputerSpeakerDeviceChanged^ cb);
	};

	private ref class CAudioSettingContextDotNetWrap sealed : public IAudioSettingContextDotNetWrap
	{
	public:
		static property CAudioSettingContextDotNetWrap^ Instance
		{
			CAudioSettingContextDotNetWrap^ get() { return m_Instance; }
		}

		void BindEvent();
		void procComputerMicDeviceChanged(array<IMicInfoDotNetWrap^ >^ newMics);
		void procComputerSpeakerDeviceChanged(array<ISpeakerInfoDotNetWrap^ >^ newSpeakers);

		virtual array<IMicInfoDotNetWrap^ >^ GetMicList();
		virtual SDKError SelectMic(String^ deviceId, String^ deviceName);
		virtual array<ISpeakerInfoDotNetWrap^ >^ GetSpeakerList();
		virtual SDKError SelectSpeaker(String^ deviceId, String^ deviceName);
		virtual SDKError EnableAutoJoinAudio(bool bEnable);
		virtual bool IsAutoJoinAudioEnabled();
		virtual SDKError EnableAutoAdjustMic(bool bEnable);
		virtual bool IsAutoAdjustMicEnabled();
		virtual SDKError EnableStereoAudio(bool bEnable);
		virtual bool IsStereoAudioEnable();
		virtual SDKError EnableMicOriginalInput(bool bEnable);
		virtual bool IsMicOriginalInputEnable();
		virtual SDKError EnableHoldSpaceKeyToSpeak(bool bEnable);
		virtual bool IsHoldSpaceKeyToSpeakEnabled();
		virtual SDKError EnableAlwaysMuteMicWhenJoinVoip(bool bEnable);
		virtual bool IsAlwaysMuteMicWhenJoinVoipEnabled();
//		virtual ITestAudioDeviceHelper* GetTestAudioDeviceHelper();
		virtual SDKError SetMicVol(float& value);
		virtual SDKError GetMicVol(float& value);
		virtual SDKError SetSpeakerVol(float& value);
		virtual SDKError GetSpeakerVol(float& value);

		virtual void Add_CB_onComputerMicDeviceChanged(onComputerMicDeviceChanged^ cb)
		{
			event_onComputerMicDeviceChanged += cb;
		}

		virtual void Remove_CB_onComputerMicDeviceChanged(onComputerMicDeviceChanged^ cb)
		{
			event_onComputerMicDeviceChanged -= cb;
		}

		virtual void Add_CB_onComputerSpeakerDeviceChanged(onComputerSpeakerDeviceChanged^ cb)
		{
			event_onComputerSpeakerDeviceChanged += cb;
		}

		virtual void Remove_CB_onComputerSpeakerDeviceChanged(onComputerSpeakerDeviceChanged^ cb)
		{
			event_onComputerSpeakerDeviceChanged -= cb;
		}
		static array<IMicInfoDotNetWrap^>^ ConvertMicList(ZOOM_SDK_NAMESPACE::IList<ZOOM_SDK_NAMESPACE::IMicInfo*>* pList);
		static array<ISpeakerInfoDotNetWrap^>^ ConvertSpeakerList(ZOOM_SDK_NAMESPACE::IList<ZOOM_SDK_NAMESPACE::ISpeakerInfo*>* pList);
	private:
		event onComputerMicDeviceChanged^ event_onComputerMicDeviceChanged;
		event onComputerSpeakerDeviceChanged^ event_onComputerSpeakerDeviceChanged;
		CAudioSettingContextDotNetWrap() {}
		virtual ~CAudioSettingContextDotNetWrap() {}
		static CAudioSettingContextDotNetWrap^ m_Instance = gcnew CAudioSettingContextDotNetWrap;
	};

	public interface class IRecordingSettingContextDotNetWrap
	{
	public:
		SDKError SetRecordingPath(String^ szPath);
		String^  GetRecordingPath();
	};

	private ref class CRecordingSettingContextDotNetWrap sealed : public IRecordingSettingContextDotNetWrap
	{
	public:
		static property CRecordingSettingContextDotNetWrap^ Instance
		{
			CRecordingSettingContextDotNetWrap^ get() { return m_Instance; }
		}

		virtual SDKError SetRecordingPath(String^ szPath);
		virtual String^  GetRecordingPath();
	private:
		CRecordingSettingContextDotNetWrap() {}
		virtual ~CRecordingSettingContextDotNetWrap() {}
		static CRecordingSettingContextDotNetWrap^ m_Instance = gcnew CRecordingSettingContextDotNetWrap;
	};

	public enum class SettingsNetWorkType : int
	{
		SETTINGS_NETWORK_WIRED = 0,
		SETTINGS_NETWORK_WIFI = 1,
		SETTINGS_NETWORK_PPP = 2,
		SETTINGS_NETWORK_3G = 3,
		SETTINGS_NETWORK_OTHERS = 4,

		SETTINGS_NETWORK_UNKNOWN = -1,
	};

	public enum class SettingConnectionType : int
	{
		SETTINGS_CONNECTION_TYPE_CLOUD = 0,
		SETTINGS_CONNECTION_TYPE_DIRECT,
		SETTINGS_CONNECTION_TYPE_UNKNOWN = -1,
	};

	public value class OverallStatisticInfo
	{
	public:
		SettingsNetWorkType net_work_type_;
		SettingConnectionType connection_type_;
		String^ proxy_addr_;
	};

	public value class AudioSessionStatisticInfo
	{
	public:
		int frequency_send_; //KHz
		int frequency_recv_; //KHz
		int latency_send_;//ms
		int latency_recv_;//ms
		int jitter_send_;//ms
		int jitter_recv_;//ms
		float packetloss_send_;//%
		float packetloss_recv_;//%
	};

	public value class ASVSessionStatisticInfo
	{
	public:
		int latency_send_;//ms
		int latency_recv_;//ms
		int jitter_send_;//ms
		int jitter_recv_;//ms
		float packetloss_send_max_;//%
		float packetloss_recv_max_;//%
		float packetloss_send_avg_;//%
		float packetloss_recv_avg_;//%
		int resolution_send_; //HIWORD->height,LOWORD->width
		int resolution_recv_; //HIWORD->height,LOWORD->width 
		int fps_send_;//fps
		int fps_recv_;//fps
	};

	public interface class IStatisticSettingContextDotNetWrap
	{
	public:
		SDKError QueryOverallStatisticInfo(OverallStatisticInfo^% info_);
		SDKError QueryAudioStatisticInfo(AudioSessionStatisticInfo^% info_);
		SDKError QueryVideoStatisticInfo(ASVSessionStatisticInfo^% info_);
		SDKError QueryShareStatisticInfo(ASVSessionStatisticInfo^% info_);
	};

	private ref class CStatisticSettingContextDotNetWrap sealed : public IStatisticSettingContextDotNetWrap
	{
	public:
		static property CStatisticSettingContextDotNetWrap^ Instance
		{
			CStatisticSettingContextDotNetWrap^ get() { return m_Instance; }
		}

		virtual SDKError QueryOverallStatisticInfo(OverallStatisticInfo^% info_);
		virtual SDKError QueryAudioStatisticInfo(AudioSessionStatisticInfo^% info_);
		virtual SDKError QueryVideoStatisticInfo(ASVSessionStatisticInfo^% info_);
		virtual SDKError QueryShareStatisticInfo(ASVSessionStatisticInfo^% info_);

	private:
		CStatisticSettingContextDotNetWrap() {}
		virtual ~CStatisticSettingContextDotNetWrap() {}
		static CStatisticSettingContextDotNetWrap^ m_Instance = gcnew CStatisticSettingContextDotNetWrap;
	};

	public interface class IShareSettingContextDotNetWrap
	{
	public:
		SDKError EnableAutoFitToWindowWhenViewSharing(bool bEnable);
		bool IsAutoFitToWindowWhenViewSharingEnabled();
		SDKError EnableAutoFullScreenVideoWhenViewShare(bool bEnable);
		bool IsAutoFullScreenVideoWhenViewShareEnabled();
		SDKError EnableTCPConnectionWhenSharing(bool bEnable);
		bool IsTCPConnectionWhenSharing();
		bool IsCurrentOSSupportAccelerateGPUWhenShare();
		SDKError EnableAccelerateGPUWhenShare(bool bEnable);
		SDKError IsAccelerateGPUWhenShareEnabled(bool& bEnable);
		SDKError EnableRemoteControlAllApplications(bool bEnable);
		bool IsRemoteControlAllApplicationsEnabled();
		SDKError EnableGreenBorderWhenShare(bool bEnable);
		bool IsGreenBorderEnabledWhenShare();
		bool IsLimitFPSEnabledWhenShare();
		SDKError EnableLimitFPSWhenShare(bool bEnable);
		SDKError EnableAnnotateHardwareAcceleration(bool bEnable);
		bool IsAnnotateHardwareAccelerationEnabled();
	};

	private ref class CShareSettingContextDotNetWrap sealed : public IShareSettingContextDotNetWrap
	{
	public:
		static property CShareSettingContextDotNetWrap^ Instance
		{
			CShareSettingContextDotNetWrap^ get() { return m_Instance; }
		}

		virtual SDKError EnableAutoFitToWindowWhenViewSharing(bool bEnable);
		virtual bool IsAutoFitToWindowWhenViewSharingEnabled();
		virtual SDKError EnableAutoFullScreenVideoWhenViewShare(bool bEnable);
		virtual bool IsAutoFullScreenVideoWhenViewShareEnabled();
		virtual SDKError EnableTCPConnectionWhenSharing(bool bEnable);
		virtual bool IsTCPConnectionWhenSharing();
		virtual bool IsCurrentOSSupportAccelerateGPUWhenShare();
		virtual SDKError EnableAccelerateGPUWhenShare(bool bEnable);
		virtual SDKError IsAccelerateGPUWhenShareEnabled(bool& bEnable);
		virtual SDKError EnableRemoteControlAllApplications(bool bEnable);
		virtual bool IsRemoteControlAllApplicationsEnabled();
		virtual SDKError EnableGreenBorderWhenShare(bool bEnable);
		virtual bool IsGreenBorderEnabledWhenShare();
		virtual bool IsLimitFPSEnabledWhenShare();
		virtual SDKError EnableLimitFPSWhenShare(bool bEnable);
		virtual SDKError EnableAnnotateHardwareAcceleration(bool bEnable);
		virtual bool IsAnnotateHardwareAccelerationEnabled();

	private:
		CShareSettingContextDotNetWrap() {}
		virtual ~CShareSettingContextDotNetWrap() {}
		static CShareSettingContextDotNetWrap^ m_Instance = gcnew CShareSettingContextDotNetWrap;
	};

	public interface class ISettingServiceDotNetWrap
	{
	public:
		SDKError ShowSettingDlg(ShowSettingDlgParam^% param);
		SDKError HideSettingDlg();
		IGeneralSettingContextDotNetWrap^ GetGeneralSettings();
		IAudioSettingContextDotNetWrap^ GetAudioSettings();
		IRecordingSettingContextDotNetWrap^ GetRecordingSettings();
		IStatisticSettingContextDotNetWrap^ GetStatisticSettings();
		IVideoSettingContextDotNetWrap^ GetVideoSettings();
	};

	private ref class CSettingServiceDotNetWrap sealed : public ISettingServiceDotNetWrap
	{
	public:
		static property CSettingServiceDotNetWrap^ Instance
		{
			CSettingServiceDotNetWrap^ get() { return m_Instance; }
		}

		void BindEvent() {}

		virtual SDKError ShowSettingDlg(ShowSettingDlgParam^% param);
		virtual SDKError HideSettingDlg();
		virtual IGeneralSettingContextDotNetWrap^ GetGeneralSettings();
		virtual IAudioSettingContextDotNetWrap^ GetAudioSettings();
		virtual IRecordingSettingContextDotNetWrap^ GetRecordingSettings();
		virtual IStatisticSettingContextDotNetWrap^ GetStatisticSettings();
		virtual IVideoSettingContextDotNetWrap^ GetVideoSettings();

	private:
		CSettingServiceDotNetWrap() {}
		virtual ~CSettingServiceDotNetWrap() {}
		static CSettingServiceDotNetWrap^ m_Instance = gcnew CSettingServiceDotNetWrap;
	};
}