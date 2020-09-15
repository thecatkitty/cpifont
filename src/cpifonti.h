#pragma once

#include <cpi.h>
#include <cpifont.h>


extern bool _get_file_header(
  const cpifont_stream       *s,
        cpi_file_header      *header);
extern bool _matches_tag(
  const char                 *str,
  const char                 *tag);
