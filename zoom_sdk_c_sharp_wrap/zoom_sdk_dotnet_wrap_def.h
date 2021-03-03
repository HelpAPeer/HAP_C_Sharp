#pragma once
using namespace System;
//ref zoom_sdk_def.h
namespace ZOOM_SDK_DOTNET_WRAP {

	public delegate void onWindowMsgNotification(unsigned int uMsg, UIntPtr wParam, IntPtr lParam);

	public enum class SDKError : int
	{
		SDKERR_SUCCESS = 0,///< Success Result
		SDKERR_NO_IMPL,///< Not support this feature now 
		SDKERR_WRONG_USEAGE,///< Wrong useage about this feature 
		SDKERR_INVALID_PARAMETER,///< Wrong parameter 
		SDKERR_MODULE_LOAD_FAILED,///< Load module failed 
		SDKERR_MEMORY_FAILED,///< No memory allocated 
		SDKERR_SERVICE_FAILED,///< Internal service error 
		SDKERR_UNINITIALIZE,///< Not initialize before use 
		SDKERR_UNAUTHENTICATION,///< Not Authentication before use
		SDKERR_NORECORDINGINPROCESS,///< No recording in process
		SDKERR_TRANSCODER_NOFOUND,///< can't find transcoder module
		SDKERR_VIDEO_NOTREADY,///< Video service not ready
		SDKERR_NO_PERMISSION,///< No premission to do this
		SDKERR_UNKNOWN,///< Unknown error 
		SDKERR_OTHER_SDK_INSTANCE_RUNNING,
	};

	public enum class SDK_LANGUAGE_ID : int
	{
		LANGUAGE_Unknow = 0,
		LANGUAGE_English,
		LANGUAGE_Chinese_Simplified,
		LANGUAGE_Chinese_Traditional,
		LANGUAGE_Japanese,
		LANGUAGE_Spanish,
		LANGUAGE_German,
		LANGUAGE_French,
		LANGUAGE_Portuguese,
		LANGUAGE_Russian,
	};

	public enum class CustomizedLanguageType : int
	{
		CustomizedLanguage_None,
		CustomizedLanguage_FilePath,
		CustomizedLanguage_Content,
	};

	public value class CustomizedLanguageInfo sealed
	{
	public:
		String^ language_name;
		String^ language_info;
		CustomizedLanguageType langType;
	};

	public enum class OptionalFeatureFlag : int
	{
		OptionalFeature_EnableCustomizedUI = (1 << 5)
	};

	public value class ConfigurableOptions sealed
	{
	public:
		CustomizedLanguageInfo customized_language;
		int optionalFeatures;
	};

	public value class InitParam sealed
	{
	public:
		String^ sdk_dll_path;
		String^ web_domain;///< Web Domain
		String^ brand_name;///< Branding name
		String^ support_url;///< Support Url
		void* res_instance;///< resource moudle handle
		unsigned int window_small_icon_id;///< windows small icon file path
		unsigned int window_big_icon_id;///< windows small icon file path
		SDK_LANGUAGE_ID language_id;///< sdk language ID
		ConfigurableOptions config_opts;
		bool enable_log;
	};

	public value class HWNDDotNet sealed
	{
	public:
		UInt32 value;
	};

	public value class RECT sealed
	{
	public:
		int Left;
		int Top;
		int Right;
		int Bottom;
	};

	public enum class SDKViewType : int
	{
		SDK_FIRST_VIEW,
		SDK_SECOND_VIEW,
	};
	
	public enum class AudioType : int
	{
		AUDIOTYPE_NONE,
		AUDIOTYPE_VOIP,
		AUDIOTYPE_PHONE,
		AUDIOTYPE_UNKNOW,
	};

	public enum class UserRole : int
	{
		USERROLE_NONE,
		USERROLE_HOST,
		USERROLE_COHOST,
		USERROLE_PANELIST,
		USERROLE_BREAKOUTROOM_MODERATOR,
		USERROLE_ATTENDEE,
	};

	public interface class IUserInfoDotNetWrap
	{
	public:
		String^ GetUserName();
		bool IsHost();
		unsigned int GetUserID();
		bool IsVideoOn();
		bool IsAudioMuted();
		AudioType GetAudioJoinType();
		bool IsMySelf();
		bool IsInWaitingRoom();
		bool IsRaiseHand();
		UserRole GetUserRole();
		bool IsPurePhoneUser();
	};

	public value class WndPosition sealed
	{
	public:
		int left;
		int top;
		HWNDDotNet hSelfWnd;
		HWNDDotNet hParent;
	};

	public enum class SDKAnnoSaveType : int
	{
		ANNO_SAVE_NONE,
		ANNO_SAVE_PNG,
		ANNO_SAVE_PDF,
		ANNO_SAVE_PNG_MEMORY,
		ANNO_SAVE_PDF_MEMORY,
		ANNO_SAVE_BITMAP_MEMORY,
	};

	public enum class AnnotationToolType : int
	{
		ANNOTOOL_NONE_DRAWING,///<switch to mouse 

		ANNOTOOL_PEN,
		ANNOTOOL_HIGHLIGHTER,
		ANNOTOOL_AUTO_LINE,
		ANNOTOOL_AUTO_RECTANGLE,
		ANNOTOOL_AUTO_ELLIPSE,
		ANNOTOOL_AUTO_ARROW,
		ANNOTOOL_AUTO_RECTANGLE_FILL,
		ANNOTOOL_AUTO_ELLIPSE_FILL,

		ANNOTOOL_SPOTLIGHT,
		ANNOTOOL_ARROW,

		ANNOTOOL_ERASER,///<earser
	};

	public enum class AnnotationClearType : int
	{
		ANNOCLEAR_ALL,
		ANNOCLEAR_SELF,
		ANNOCLEAR_OTHER,
	};

	public enum class VideoHardwareEncodeType : int
	{
		VIDEO_HARDWARE_ENCODE_RECEIVING,
		VIDEO_HARDWARE_ENCODE_SENDING,
		VIDEO_HARDWARE_ENCODE_PROCESSING,
	};
}