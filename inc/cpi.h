#pragma once

#ifdef __cplusplus
extern "C" {
#endif
#include <stdint.h>


#ifdef _MSC_VER
#pragma warning(disable : 4200)
#endif

#pragma pack(push, 1)
typedef struct {
  char     file_tag[8];
  uint64_t _reserved;
  uint16_t pointers_count;
  int8_t   pointer_type;
  uint32_t offset;
} cpi_file_header;

typedef struct {
  uint16_t entries_count;
} cpi_font_info_header;

typedef struct {
  uint16_t header_size;
  uint32_t next_offset;
  uint16_t device_type;
  char     device_name[8];
  uint16_t codepage;
  uint16_t _reserved[3];
  uint32_t font_offset;
} cpi_codepage_entry_header;

typedef struct {
  uint16_t _reserved;
  uint16_t fonts_count;
  uint16_t data_length;
} cpi_font_data_header;

typedef struct {
  uint8_t  character_rows;
  uint8_t  character_cols;
  uint16_t _aspect_ratio;
  uint16_t characters_count;
  char     data[0];
} cpi_font_data;
#pragma pack(pop)


enum cpi_device_type {
  CPI_DEVICE_DISPLAY = 1,
  CPI_DEVICE_PRINTER = 2
};

extern const char cpi_file_tag[8];
extern const char cpi_device_name_cga[8];
extern const char cpi_device_name_ega[8];
extern const char cpi_device_name_mono[8];
extern const char cpi_device_name_lcd[8];

#ifdef __cplusplus
} // extern "C"
#endif
