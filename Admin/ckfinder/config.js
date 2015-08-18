/*
Copyright (c) 2003-2010, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckfinder.com/license
*/

CKFinder.customConfig = function( config )
{
	// Define changes to default configuration here. For example:
	// config.skin = 'v1';
    // config.language = 'fr';
    config.language = 'zh-cn';
	config.filebrowserBrowseUrl= 'ckfinder/ckfinder.html'; //上传文件时浏览服务文件夹

    config.filebrowserImageBrowseUrl= 'ckfinder/ckfinder.html?Type=Images'; //上传图片时浏览服务文件夹

    config.filebrowserFlashBrowseUrl= 'ckfinder/ckfinder.html?Type=Flash';  //上传Flash时浏览服务文件夹

    config.filebrowserUploadUrl = 'ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files'; //上传文件按钮(标签)

    config.filebrowserImageUploadUrl= 'ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images'; //上传图片按钮(标签)

    config.filebrowserFlashUploadUrl= 'ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash'; //上传Flash按钮(标签)
};
