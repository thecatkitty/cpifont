#include "app.h"

#include <iostream>


cpifont_stream cfs{
  /*read*/[](void *ctx, char *buffer, size_t bytes) -> size_t {
    auto fi = reinterpret_cast<std::istream*>(ctx);
    try {
      fi->read(buffer, bytes);
      return bytes;
    } catch(...) {
      return 0;
    }
  },
  /*write*/nullptr,
  /*flush*/nullptr,
  /*tell*/[](void *ctx) -> size_t {
    auto fi = reinterpret_cast<std::istream*>(ctx);
    return fi->tellg();
  },
  /*seek*/[](void *ctx, size_t offset, cpifont_origin origin) -> bool {
    auto fi = reinterpret_cast<std::istream*>(ctx);
    try {
      auto way =
        origin == CPIFONT_ORIGIN_BEG ? std::ios::beg :
        origin == CPIFONT_ORIGIN_CUR ? std::ios::cur : std::ios::end;
      fi->seekg(offset, way);
      return true;
    } catch(...) {
      return false;
    }
  },
  /*context*/nullptr
};
