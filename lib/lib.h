#pragma once

#include <cpi.h>
#include <cpifont.h>


extern bool _get_file_header(
  const cpifont_stream       *s,
        cpi_file_header      *header);
extern bool _matches_tag(
  const char                 *str,
  const char                 *tag);

extern const char cpi_dos_file_tag[8];
extern const char cpi_nt_file_tag[8];
extern const char cpi_device_name_cga[8];
extern const char cpi_device_name_ega[8];
extern const char cpi_device_name_mono[8];
extern const char cpi_device_name_lcd[8];
