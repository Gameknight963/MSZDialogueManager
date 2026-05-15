#pragma once
#include <windows.h>
#include <uxtheme.h>
#include <vssym32.h>
#include <d2d1.h>
#include <stdio.h>
#include "MinHook.h"

#pragma comment(lib, "d2d1.lib")
#pragma comment(lib, "uxtheme.lib")

typedef HRESULT(WINAPI* pDrawThemeBackground)(HTHEME, HDC, int, int, LPCRECT, LPCRECT);
static pDrawThemeBackground DrawThemeBackground_orig = nullptr;

typedef HRESULT(WINAPI* pDrawThemeBackgroundEx)(HTHEME, HDC, int, int, LPCRECT, const DTBGOPTS*);
static pDrawThemeBackgroundEx DrawThemeBackgroundEx_orig = nullptr;

static ID2D1Factory* g_d2dFactory = nullptr;

typedef HRESULT(WINAPI* pGetThemeClass)(HTHEME, LPWSTR, int);
static pGetThemeClass GetThemeClass = nullptr;

static HRESULT CreateBoundD2DRenderTarget(HDC hdc, LPCRECT pRect, ID2D1DCRenderTarget** ppRenderTarget)
{
    D2D1_RENDER_TARGET_PROPERTIES rtProps = D2D1::RenderTargetProperties(
        D2D1_RENDER_TARGET_TYPE_SOFTWARE,
        D2D1::PixelFormat(DXGI_FORMAT_B8G8R8A8_UNORM, D2D1_ALPHA_MODE_PREMULTIPLIED),
        NULL, NULL,
        D2D1_RENDER_TARGET_USAGE_GDI_COMPATIBLE,
        D2D1_FEATURE_LEVEL_DEFAULT
    );

    ID2D1DCRenderTarget* renderTarget = nullptr;
    HRESULT hr = g_d2dFactory->CreateDCRenderTarget(&rtProps, &renderTarget);
    if (FAILED(hr)) return hr;

    hr = renderTarget->BindDC(hdc, pRect);
    if (FAILED(hr))
    {
        renderTarget->Release();
        return hr;
    }

    *ppRenderTarget = renderTarget;
    return S_OK;
}

static BOOL PaintScroll(HDC hdc, int iPartId, int iStateId, LPCRECT pRect)
{
    if (iPartId == SBP_UPPERTRACKVERT || iPartId == SBP_LOWERTRACKVERT
        || iPartId == SBP_UPPERTRACKHORZ || iPartId == SBP_LOWERTRACKHORZ)
        return TRUE;

    if (!g_d2dFactory || (iPartId != SBP_THUMBBTNVERT && iPartId != SBP_THUMBBTNHORZ))
        return FALSE;

    ID2D1DCRenderTarget* pRenderTarget = nullptr;
    if (FAILED(CreateBoundD2DRenderTarget(hdc, pRect, &pRenderTarget)))
        return FALSE;

    float width = (float)(pRect->right - pRect->left);
    float height = (float)(pRect->bottom - pRect->top);
    float cornerRadius = 4.f;

    D2D1_COLOR_F color = (iStateId == SCRBS_NORMAL)
        ? D2D1::ColorF(0.63f, 0.63f, 0.63f, 0.5f)
        : D2D1::ColorF(0.88f, 0.88f, 0.88f, 0.63f);

    D2D1_RECT_F thumbRect;
    if (iPartId == SBP_THUMBBTNVERT)
    {
        float inset = width * 0.25f;
        thumbRect = D2D1::RectF(inset, 0, width - inset, height);
    }
    else
    {
        float inset = height * 0.25f;
        thumbRect = D2D1::RectF(0, inset, width, height - inset);
    }

    D2D1_ROUNDED_RECT rr = { thumbRect, cornerRadius, cornerRadius };

    ID2D1SolidColorBrush* brush = nullptr;
    pRenderTarget->CreateSolidColorBrush(color, &brush);

    pRenderTarget->BeginDraw();
    pRenderTarget->FillRoundedRectangle(&rr, brush);
    pRenderTarget->EndDraw();

    brush->Release();
    pRenderTarget->Release();

    return TRUE;
}

static BOOL PaintScrollArrows(HDC hdc, int iPartId, int iStateId, LPCRECT pRect)
{
    if (iPartId != SBP_ARROWBTN || !g_d2dFactory)
        return FALSE;

    ID2D1DCRenderTarget* pRenderTarget = nullptr;
    if (FAILED(CreateBoundD2DRenderTarget(hdc, pRect, &pRenderTarget)))
        return FALSE;

    float width = (float)(pRect->right - pRect->left);
    float height = (float)(pRect->bottom - pRect->top);
    float centerX = width / 2.f;
    float centerY = height / 2.f;

    float arrowLen = 4.f;
    float dx = arrowLen * 0.866f;
    float dy = arrowLen * 0.5f;

    D2D1_COLOR_F color = (iStateId == ABS_UPHOT || iStateId == ABS_DOWNHOT ||
        iStateId == ABS_LEFTHOT || iStateId == ABS_RIGHTHOT)
        ? D2D1::ColorF(0.88f, 0.88f, 0.88f, 0.75f)
        : D2D1::ColorF(0.63f, 0.63f, 0.63f, 0.5f);

    D2D1_POINT_2F ptTip, ptLeft, ptRight;

    if (iStateId >= ABS_UPNORMAL && iStateId <= ABS_UPDISABLED)
    {
        ptTip = D2D1::Point2F(centerX, centerY - dy);
        ptLeft = D2D1::Point2F(centerX - dx, centerY + dy);
        ptRight = D2D1::Point2F(centerX + dx, centerY + dy);
    }
    else if (iStateId >= ABS_DOWNNORMAL && iStateId <= ABS_DOWNDISABLED)
    {
        ptTip = D2D1::Point2F(centerX, centerY + dy);
        ptLeft = D2D1::Point2F(centerX - dx, centerY - dy);
        ptRight = D2D1::Point2F(centerX + dx, centerY - dy);
    }
    else if (iStateId >= ABS_LEFTNORMAL && iStateId <= ABS_LEFTDISABLED)
    {
        ptTip = D2D1::Point2F(centerX - dy, centerY);
        ptLeft = D2D1::Point2F(centerX + dy, centerY - dx);
        ptRight = D2D1::Point2F(centerX + dy, centerY + dx);
    }
    else if (iStateId >= ABS_RIGHTNORMAL && iStateId <= ABS_RIGHTDISABLED)
    {
        ptTip = D2D1::Point2F(centerX + dy, centerY);
        ptLeft = D2D1::Point2F(centerX - dy, centerY - dx);
        ptRight = D2D1::Point2F(centerX - dy, centerY + dx);
    }
    else
    {
        pRenderTarget->Release();
        return FALSE;
    }

    ID2D1SolidColorBrush* brush = nullptr;
    ID2D1SolidColorBrush* bgBrush = nullptr;
    pRenderTarget->CreateSolidColorBrush(color, &brush);
    pRenderTarget->CreateSolidColorBrush(D2D1::ColorF(0.12f, 0.12f, 0.12f, 1.f), &bgBrush);

    D2D1_RECT_F bg = D2D1::RectF(0, 0, width, height);

    pRenderTarget->BeginDraw();
    pRenderTarget->FillRectangle(&bg, bgBrush);
    pRenderTarget->DrawLine(ptLeft, ptTip, brush, 1.5f);
    pRenderTarget->DrawLine(ptRight, ptTip, brush, 1.5f);
    pRenderTarget->EndDraw();

    brush->Release();
    bgBrush->Release();
    pRenderTarget->Release();

    return TRUE;
}

static HRESULT WINAPI HookedDrawThemeBackground(
    HTHEME hTheme, HDC hdc, int iPartId, int iStateId,
    LPCRECT pRect, LPCRECT pClipRect)
{
    if (GetThemeClass)
    {
        wchar_t className[256] = {};
        GetThemeClass(hTheme, className, 256);
        if (wcscmp(className, L"ScrollBar") == 0)
        {
            if (PaintScroll(hdc, iPartId, iStateId, pRect))
                return S_OK;
            if (PaintScrollArrows(hdc, iPartId, iStateId, pRect))
                return S_OK;
        }
    }
    return DrawThemeBackground_orig(hTheme, hdc, iPartId, iStateId, pRect, pClipRect);
}

public ref class ScrollHook
{
public:
    static void Install()
    {
        D2D1CreateFactory(D2D1_FACTORY_TYPE_MULTI_THREADED,
            __uuidof(ID2D1Factory),
            nullptr,
            reinterpret_cast<void**>(&g_d2dFactory));

        GetThemeClass = (pGetThemeClass)GetProcAddress(
            GetModuleHandleW(L"uxtheme.dll"), MAKEINTRESOURCEA(74));

        MH_Initialize();

        void* pTarget = GetProcAddress(
            GetModuleHandleW(L"uxtheme.dll"), "DrawThemeBackground");
        MH_CreateHook(pTarget, &HookedDrawThemeBackground,
            reinterpret_cast<void**>(&DrawThemeBackground_orig));
        MH_EnableHook(pTarget);
    }

    static void Uninstall()
    {
        MH_DisableHook(MH_ALL_HOOKS);
        MH_Uninitialize();
        if (g_d2dFactory) { g_d2dFactory->Release(); g_d2dFactory = nullptr; }
    }
};