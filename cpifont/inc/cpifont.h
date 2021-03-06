#pragma once

#ifdef __cplusplus
extern "C" {
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>

#include "cpifont_exports.h"

#ifdef _MSC_VER
#define CPIFONTAPI __cdecl
#elif defined(__i386__)
#define CPIFONTAPI __attribute__((cdecl))
#else
#define CPIFONTAPI
#endif

typedef enum {
  CPIFONT_TYPE_UNKNOWN = 0,
  CPIFONT_TYPE_DOS     = 1,
  CPIFONT_TYPE_NT      = 2
} cpifont_type;

typedef enum {
  CPIFONT_DEVICE_UNKNOWN = 0x0000,
  CPIFONT_DEVICE_EGA     = 0x1000,
  CPIFONT_DEVICE_IBM5140 = 0x1001,
  CPIFONT_DEVICE_CGA     = 0x1002,
  CPIFONT_DEVICE_MONO    = 0x1003,
  CPIFONT_DEVICE_IBM4201 = 0x2000,
  CPIFONT_DEVICE_IBM4208 = 0x2001,
  CPIFONT_DEVICE_IBM5205 = 0x2002,
  CPIFONT_DEVICE_IBM1050 = 0x2003,
  CPIFONT_DEVICE_EPS     = 0x2004,
  CPIFONT_DEVICE_PPDS    = 0x2005
} cpifont_device;

typedef enum {
  CPIFONT_DEVICE_TYPE_UNKNOWN = 0,
  CPIFONT_DEVICE_TYPE_DISPLAY = 1,
  CPIFONT_DEVICE_TYPE_PRINTER = 2
} cpifont_device_type;

typedef enum {
  CPIFONT_ORIGIN_BEG = 0,
  CPIFONT_ORIGIN_CUR = 1,
  CPIFONT_ORIGIN_END = 2
} cpifont_origin;

typedef enum {
  CPIFONT_OK             = 0x000,
  CPIFONT_LAST           = 0x001,
  CPIFONT_RANGE_ERROR    = 0x010,
  CPIFONT_VALUE_ERROR    = 0x011,
  CPIFONT_STREAM_ERROR   = 0x100,
  CPIFONT_STREAM_EOF     = 0x101,
  CPIFONT_STREAM_RANGE   = 0x102,
  CPIFONT_STREAM_FATAL   = 0x1FF,
  CPIFONT_UNKNOWN_TYPE   = 0x200,
  CPIFONT_INVALID_FORMAT = 0x201
} cpifont_status;


typedef struct {
  cpifont_status (CPIFONTAPI *read)(void *ctx, char *buffer, size_t bytes);
  cpifont_status (CPIFONTAPI *write)(void *ctx, char *buffer, size_t bytes);
  cpifont_status (CPIFONTAPI *flush)(void *ctx);
  size_t         (CPIFONTAPI *tell)(void *ctx);
  cpifont_status (CPIFONTAPI *seek)(void *ctx, size_t offset, cpifont_origin origin);

  void *context;
} cpifont_stream;

typedef struct {
  size_t              next_offset;
  cpifont_type        file_type;
  cpifont_device      device;
  cpifont_device_type device_type;
  char                device_name[9];
  uint16_t            codepage;
  uint16_t            fonts;
  size_t              fonts_offset;
  size_t              fonts_size;
} cpifont_entry_info;

typedef struct {
  uint8_t  glyph_width;
  uint8_t  glyph_height;
  uint16_t glyphs;
  size_t   bitmap_offset;
  size_t   bitmap_size;
} cpifont_font_info;


cpifont_status CPIFONT_EXPORTS CPIFONTAPI cpifont_get_type(
  const cpifont_stream       *s,
        cpifont_type         *type);
cpifont_status CPIFONT_EXPORTS CPIFONTAPI cpifont_get_entry_count(
  const cpifont_stream       *s,
        int                  *entry_count);

cpifont_status CPIFONT_EXPORTS CPIFONTAPI cpifont_get_next_entry(
  const cpifont_stream       *s,
        cpifont_entry_info   *entry);
cpifont_status CPIFONT_EXPORTS CPIFONTAPI cpifont_get_next_font(
  const cpifont_stream       *s,
  const cpifont_entry_info   *entry,
        cpifont_font_info    *font);

cpifont_status CPIFONT_EXPORTS CPIFONTAPI cpifont_get_glyph(
  const cpifont_stream       *s,
  const cpifont_font_info    *font,
        size_t               index,
        char                 *glyph);

const char CPIFONT_EXPORTS * CPIFONTAPI cpifont_get_type_string(
        cpifont_type         type);
const char CPIFONT_EXPORTS * CPIFONTAPI cpifont_get_device_string(
        cpifont_device       device);
const char CPIFONT_EXPORTS * CPIFONTAPI cpifont_get_device_type_string(
        cpifont_device_type  device_type);

#ifdef __cplusplus
} // extern "C"
#endif
