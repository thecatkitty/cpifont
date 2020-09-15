#include "lib.h"


#define CASE_STR(x) case (x): return (#x)


const char cpi_file_tag[8] = {'\xFF', 'F', 'O', 'N', 'T', ' ', ' ', ' '};

const char cpi_device_name_cga[8] = {'C', 'G', 'A', ' ', ' ', ' ', ' ', ' '};
const char cpi_device_name_ega[8] = {'E', 'G', 'A', ' ', ' ', ' ', ' ', ' '};
const char cpi_device_name_mono[8] = {'M', 'O', 'N', 'O', ' ', ' ', ' ', ' '};
const char cpi_device_name_lcd[8] = {'L', 'C', 'D', ' ', ' ', ' ', ' ', ' '};


const char *cpifont_get_device_string(
        cpifont_device       device)
{
  switch (device) {
    CASE_STR(CPIFONT_DEVICE_EGA);
    CASE_STR(CPIFONT_DEVICE_LCD);
    CASE_STR(CPIFONT_DEVICE_CGA);
    CASE_STR(CPIFONT_DEVICE_MONO);
    default: return "unknown";
  }
}

const char *cpifont_get_device_type_string(
        cpifont_device_type  device_type)
{
  switch (device_type) {
    CASE_STR(CPIFONT_DEVICE_TYPE_DISPLAY);
    CASE_STR(CPIFONT_DEVICE_TYPE_PRINTER);
    default: return "unknown";
  }
}
