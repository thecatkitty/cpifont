#include "lib.h"


#define CASE_STR(x) case (x): return (#x)


const char cpi_dos_file_tag[8] = {'\xFF', 'F', 'O', 'N', 'T', ' ', ' ', ' '};
const char cpi_nt_file_tag[8] = {'\xFF', 'F', 'O', 'N', 'T', '.', 'N', 'T'};

const char cpi_device_name_cga[8] = {'C', 'G', 'A', ' ', ' ', ' ', ' ', ' '};
const char cpi_device_name_ega[8] = {'E', 'G', 'A', ' ', ' ', ' ', ' ', ' '};
const char cpi_device_name_mono[8] = {'M', 'O', 'N', 'O', ' ', ' ', ' ', ' '};
const char cpi_device_name_lcd[8] = {'L', 'C', 'D', ' ', ' ', ' ', ' ', ' '};
const char cpi_device_name_4201[8] = {'4', '2', '0', '1', ' ', ' ', ' ', ' '};
const char cpi_device_name_4208[8] = {'4', '2', '0', '8', ' ', ' ', ' ', ' '};
const char cpi_device_name_5205[8] = {'5', '2', '0', '5', ' ', ' ', ' ', ' '};
const char cpi_device_name_1050[8] = {'1', '0', '5', '0', ' ', ' ', ' ', ' '};
const char cpi_device_name_eps[8] = {'E', 'P', 'S', ' ', ' ', ' ', ' ', ' '};
const char cpi_device_name_ppds[8] = {'P', 'P', 'D', 'S', ' ', ' ', ' ', ' '};


const char *cpifont_get_type_string(
        cpifont_type         type)
{
  switch (type) {
    CASE_STR(CPIFONT_TYPE_DOS);
    CASE_STR(CPIFONT_TYPE_NT);
    default: return "unknown";
  }
}

const char *cpifont_get_device_string(
        cpifont_device       device)
{
  switch (device) {
    CASE_STR(CPIFONT_DEVICE_EGA);
    CASE_STR(CPIFONT_DEVICE_IBM5140);
    CASE_STR(CPIFONT_DEVICE_CGA);
    CASE_STR(CPIFONT_DEVICE_MONO);
    CASE_STR(CPIFONT_DEVICE_IBM4201);
    CASE_STR(CPIFONT_DEVICE_IBM4208);
    CASE_STR(CPIFONT_DEVICE_IBM5205);
    CASE_STR(CPIFONT_DEVICE_IBM1050);
    CASE_STR(CPIFONT_DEVICE_EPS);
    CASE_STR(CPIFONT_DEVICE_PPDS);

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
