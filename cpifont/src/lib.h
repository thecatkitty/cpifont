#pragma once

#include <cpi.h>
#include <cpifont.h>


extern bool _matches_tag(
  const char                 *str,
  const char                 *tag);

extern const char cpi_dos_file_tag[8];
extern const char cpi_nt_file_tag[8];
extern const char cpi_device_name_cga[8];
extern const char cpi_device_name_ega[8];
extern const char cpi_device_name_mono[8];
extern const char cpi_device_name_lcd[8];
extern const char cpi_device_name_4201[8];
extern const char cpi_device_name_4208[8];
extern const char cpi_device_name_5205[8];
extern const char cpi_device_name_1050[8];
extern const char cpi_device_name_eps[8];
extern const char cpi_device_name_ppds[8];
