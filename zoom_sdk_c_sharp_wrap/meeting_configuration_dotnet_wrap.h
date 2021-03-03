#pragma once
using namespace System;
using namespace System::Windows;
#include "zoom_sdk_dotnet_wrap_def.h"
namespace ZOOM_SDK_DOTNET_WRAP {

	public enum class RequiredInfoType : int
	{
		REQUIRED_INFO_TYPE_NONE,
		REQUIRED_INFO_TYPE_Password, ///< if you want to join meeting, you need input password
		REQUIRED_INFO_TYPE_Password4WrongPassword,///< you input a wrong password, please input again
		REQUIRED_INFO_TYPE_PasswordAndScreenName,///< if you want to join meeting, you need input password and screen name
	};

	public interface class IMeetingPasswordAndScreenNameHandler
	{
	public:
		RequiredInfoType GetRequiredInfoType();
		bool InputMeetingPasswordAndScreenName(String^ meetingPassword, String^ screenName);
		void Cancel();
	};
	/*
	//this one may be defined in another file. If so, remove this defination
	//just leave it here to make sure we have the class defined
	public value class WndPosition sealed
	{
	public:
		int left;
		int top;
		HWNDDotNet hSelfWnd;
		HWNDDotNet hParent;
	};
	*/
	public delegate void onInputMeetingPasswordAndScreenNameNotification(IMeetingPasswordAndScreenNameHandler^ pHandler);
	public delegate void onAirPlayInstructionWndNotification(bool bShow, String^ airhostName);

	public interface class IMeetingConfigurationDotNetWrap
	{
	public:
		void Reset();
		void SetFloatVideoPos(WndPosition pos);
		void SetSharingToolbarVisibility(bool bShow);
		void SetBottomFloatToolbarWndVisibility(bool bShow);
		void SetDirectShareMonitorID(String^ monitorID);
		void SetMeetingUIPos(WndPosition pos);
		void DisableWaitingForHostDialog(bool bDisable);
		void DisablePopupMeetingWrongPSWDlg(bool bDisable);
		void EnableAutoEndOtherMeetingWhenStartMeeting(bool bEnable);
		void EnableAutoAdjustSpeakerVolumeWhenJoinAudio(bool bEnable);
		void EnableAutoAdjustMicVolumeWhenJoinAudio(bool bEnable);
		void EnableApproveRemoteControlDlg(bool bEnable);
		void EnableDeclineRemoteControlResponseDlg(bool bEnable);
		void EnableLeaveMeetingOptionForHost(bool bEnable);
		void EnableInviteButtonOnMeetingUI(bool bEnable);
		void EnableInputMeetingPasswordDlg(bool bEnable);
		void EnableEnterAndExitFullScreenButtonOnMeetingUI(bool bEnable);
		void EnableLButtonDBClick4SwitchFullScreenMode(bool bEnable);
		void SetFloatVideoWndVisibility(bool bShow);
		void PrePopulateWebinarRegistrationInfo(String^ email, String^ username);
		void RedirectClickShareBTNEvent(bool bRedirect);
		void RedirectClickEndMeetingBTNEvent(bool bRedirect);
		void EnableToolTipsShow(bool bEnable);
		void EnableAirplayInstructionWindow(bool bEnable);
		void EnableClaimHostFeature(bool bEnable);
		void ConfigDSCP(int dscpAudio, int dscpVideo, bool bReset);
		void RedirectClickParticipantListBTNEvent(bool bRedirect);
		void DisableRemoteCtrlCopyPasteFeature(bool bDisable);
		void DisableSplitScreenModeUIElements(bool bDisable);
		void EnableAutoHideJoinAudioDialog(bool bEnable);
		void EnableHideFullPhoneNumber4PureCallinUser(bool bHide);
		void EnableLengthLimitationOfMeetingNumber(bool bEnable);
		void EnableShareIOSDevice(bool bEnable);
		void RedirectClickCustomLiveStreamMenuEvent(bool bRedirect);
		void EnableInputMeetingScreenNameDlg(bool bEnable);
		void DisableShowJoinMeetingWnd(bool bDisable);

		void Add_CB_onInputMeetingPasswordAndScreenNameNotification(onInputMeetingPasswordAndScreenNameNotification^ cb);
		void Add_CB_onAirPlayInstructionWndNotification(onAirPlayInstructionWndNotification^ cb);
		void Remove_CB_onInputMeetingPasswordAndScreenNameNotification(onInputMeetingPasswordAndScreenNameNotification^ cb);
		void Remove_CB_onAirPlayInstructionWndNotification(onAirPlayInstructionWndNotification^ cb);
	};

	private ref class CMeetingConfigurationDotNetWrap sealed : public IMeetingConfigurationDotNetWrap
	{
		// TODO: Add your methods for this class here.
	public:
		static property CMeetingConfigurationDotNetWrap^ Instance
		{
			CMeetingConfigurationDotNetWrap^ get() { return m_Instance; }
		}

		virtual void Reset();
		virtual void SetFloatVideoPos(WndPosition pos);
		virtual void SetSharingToolbarVisibility(bool bShow);
		virtual void SetBottomFloatToolbarWndVisibility(bool bShow);
		virtual void SetDirectShareMonitorID(String^ monitorID);
		virtual void SetMeetingUIPos(WndPosition pos);
		virtual void DisableWaitingForHostDialog(bool bDisable);
		virtual void DisablePopupMeetingWrongPSWDlg(bool bDisable);
		virtual void EnableAutoEndOtherMeetingWhenStartMeeting(bool bEnable);
		virtual void EnableAutoAdjustSpeakerVolumeWhenJoinAudio(bool bEnable);
		virtual void EnableAutoAdjustMicVolumeWhenJoinAudio(bool bEnable);
		virtual void EnableApproveRemoteControlDlg(bool bEnable);
		virtual void EnableDeclineRemoteControlResponseDlg(bool bEnable);
		virtual void EnableLeaveMeetingOptionForHost(bool bEnable);
		virtual void EnableInviteButtonOnMeetingUI(bool bEnable);
		virtual void EnableInputMeetingPasswordDlg(bool bEnable);
		virtual void EnableEnterAndExitFullScreenButtonOnMeetingUI(bool bEnable);
		virtual void EnableLButtonDBClick4SwitchFullScreenMode(bool bEnable);
		virtual void SetFloatVideoWndVisibility(bool bShow);
		virtual void PrePopulateWebinarRegistrationInfo(String^ email, String^ username);
		virtual void RedirectClickShareBTNEvent(bool bRedirect);
		virtual void RedirectClickEndMeetingBTNEvent(bool bRedirect);
		virtual void EnableToolTipsShow(bool bEnable);
		virtual void EnableAirplayInstructionWindow(bool bEnable);
		virtual void EnableClaimHostFeature(bool bEnable);
		virtual void ConfigDSCP(int dscpAudio, int dscpVideo, bool bReset);
		virtual void RedirectClickParticipantListBTNEvent(bool bRedirect);
		virtual void DisableRemoteCtrlCopyPasteFeature(bool bDisable);
		virtual void DisableSplitScreenModeUIElements(bool bDisable);
		virtual void EnableAutoHideJoinAudioDialog(bool bEnable);
		virtual void EnableHideFullPhoneNumber4PureCallinUser(bool bHide);
		virtual void EnableLengthLimitationOfMeetingNumber(bool bEnable);
		virtual void EnableShareIOSDevice(bool bEnable);
		virtual void DisableShowJoinMeetingWnd(bool bDisable);
		virtual void RedirectClickCustomLiveStreamMenuEvent(bool bRedirect);
		virtual void EnableInputMeetingScreenNameDlg(bool bEnable);

		virtual void Add_CB_onInputMeetingPasswordAndScreenNameNotification(onInputMeetingPasswordAndScreenNameNotification^ cb)
		{
			event_onInputMeetingPasswordAndScreenNameNotification += cb;
		}

		virtual void Remove_CB_onInputMeetingPasswordAndScreenNameNotification(onInputMeetingPasswordAndScreenNameNotification^ cb)
		{
			event_onInputMeetingPasswordAndScreenNameNotification -= cb;
		}

		virtual void Add_CB_onAirPlayInstructionWndNotification(onAirPlayInstructionWndNotification^ cb)
		{
			event_onAirPlayInstructionWndNotification += cb;
		}

		virtual void Remove_CB_onAirPlayInstructionWndNotification(onAirPlayInstructionWndNotification^ cb)
		{
			event_onAirPlayInstructionWndNotification -= cb;
		}

		void BindEvent();
		void ProcInputMeetingPasswordAndScreenNameNotification(IMeetingPasswordAndScreenNameHandler^ pHandler);
		void ProcAirPlayInstructionWndNotification(bool bShow, String^ airhostName);

	private:
		CMeetingConfigurationDotNetWrap() {};
		virtual ~CMeetingConfigurationDotNetWrap() {};
		event onInputMeetingPasswordAndScreenNameNotification^ event_onInputMeetingPasswordAndScreenNameNotification;
		event onAirPlayInstructionWndNotification^ event_onAirPlayInstructionWndNotification;
		static CMeetingConfigurationDotNetWrap^ m_Instance = gcnew CMeetingConfigurationDotNetWrap;
	};
}
