set(PROJECT_NAME annaCore3)

################################################################################
# Source groups
################################################################################
set(Header_Files
    "AnnaExtension.h"
    "Core.h"
    "Helper.hpp"
    "Output.h"
    "Response.h"
)
source_group("Header Files" FILES ${Header_Files})

set(Source_Files
    "AnnaExtension.cpp"
    "Core.cpp"
    "Output.cpp"
    "Response.cpp"
)
source_group("Source Files" FILES ${Source_Files})

set(ALL_FILES
    ${Header_Files}
    ${Source_Files}
)

################################################################################
# Target
################################################################################
add_executable(${PROJECT_NAME} ${ALL_FILES})
set_target_properties(${PROJECT_NAME} PROPERTIES FOLDER "Core")

use_props(${PROJECT_NAME} "${CMAKE_CONFIGURATION_TYPES}" "${DEFAULT_CXX_PROPS}")
set(ROOT_NAMESPACE annaCore3)

set_target_properties(${PROJECT_NAME} PROPERTIES
    VS_GLOBAL_KEYWORD "Win32Proj"
)
if("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "Any CPU")
    set_target_properties(${PROJECT_NAME} PROPERTIES
        INTERPROCEDURAL_OPTIMIZATION_RELEASE  "TRUE"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "ARM")
    set_target_properties(${PROJECT_NAME} PROPERTIES
        INTERPROCEDURAL_OPTIMIZATION_RELEASE  "TRUE"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "ARM64")
    set_target_properties(${PROJECT_NAME} PROPERTIES
        INTERPROCEDURAL_OPTIMIZATION_RELEASE  "TRUE"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "iPhone")
    set_target_properties(${PROJECT_NAME} PROPERTIES
        INTERPROCEDURAL_OPTIMIZATION_RELEASE  "TRUE"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "iPhoneSimulator")
    set_target_properties(${PROJECT_NAME} PROPERTIES
        INTERPROCEDURAL_OPTIMIZATION_RELEASE  "TRUE"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "x64")
    set_target_properties(${PROJECT_NAME} PROPERTIES
        INTERPROCEDURAL_OPTIMIZATION_RELEASE  "TRUE"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "x86")
    set_target_properties(${PROJECT_NAME} PROPERTIES
        INTERPROCEDURAL_OPTIMIZATION_RELEASE  "TRUE"
    )
endif()
################################################################################
# Include directories
################################################################################
if("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "Any CPU")
    target_include_directories(${PROJECT_NAME} PUBLIC
        "$<$<CONFIG:Debug>:"
            "${CMAKE_CURRENT_SOURCE_DIR}/../../../boost_1_77_0"
        ">"
        "$<$<CONFIG:Release>:"
            "${CMAKE_CURRENT_SOURCE_DIR}/../../../boost_1_77_0"
        ">"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "ARM")
    target_include_directories(${PROJECT_NAME} PUBLIC
        "$<$<CONFIG:Debug>:"
            "${CMAKE_CURRENT_SOURCE_DIR}/../../../boost_1_77_0"
        ">"
        "$<$<CONFIG:Release>:"
            "${CMAKE_CURRENT_SOURCE_DIR}/../../../boost_1_77_0"
        ">"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "ARM64")
    target_include_directories(${PROJECT_NAME} PUBLIC
        "$<$<CONFIG:Debug>:"
            "${CMAKE_CURRENT_SOURCE_DIR}/../../../boost_1_77_0"
        ">"
        "$<$<CONFIG:Release>:"
            "${CMAKE_CURRENT_SOURCE_DIR}/../../../boost_1_77_0"
        ">"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "iPhone")
    target_include_directories(${PROJECT_NAME} PUBLIC
        "$<$<CONFIG:Debug>:"
            "${CMAKE_CURRENT_SOURCE_DIR}/../../../boost_1_77_0"
        ">"
        "$<$<CONFIG:Release>:"
            "${CMAKE_CURRENT_SOURCE_DIR}/../../../boost_1_77_0"
        ">"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "iPhoneSimulator")
    target_include_directories(${PROJECT_NAME} PUBLIC
        "$<$<CONFIG:Debug>:"
            "${CMAKE_CURRENT_SOURCE_DIR}/../../../boost_1_77_0"
        ">"
        "$<$<CONFIG:Release>:"
            "${CMAKE_CURRENT_SOURCE_DIR}/../../../boost_1_77_0"
        ">"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "x64")
    target_include_directories(${PROJECT_NAME} PUBLIC
        "${CMAKE_CURRENT_SOURCE_DIR}/../../../boost_1_77_0"
    )
endif()

################################################################################
# Compile definitions
################################################################################
if("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "Any CPU")
    target_compile_definitions(${PROJECT_NAME} PRIVATE
        "$<$<CONFIG:AdHoc>:"
            "WIN32;"
            "_DEBUG"
        ">"
        "$<$<CONFIG:AppStore>:"
            "WIN32;"
            "_DEBUG"
        ">"
        "$<$<CONFIG:Debug>:"
            "_DEBUG"
        ">"
        "$<$<CONFIG:Release>:"
            "NDEBUG"
        ">"
        "_CONSOLE;"
        "UNICODE;"
        "_UNICODE"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "ARM")
    target_compile_definitions(${PROJECT_NAME} PRIVATE
        "$<$<CONFIG:AdHoc>:"
            "WIN32;"
            "_DEBUG"
        ">"
        "$<$<CONFIG:AppStore>:"
            "WIN32;"
            "_DEBUG"
        ">"
        "$<$<CONFIG:Debug>:"
            "_DEBUG"
        ">"
        "$<$<CONFIG:Release>:"
            "NDEBUG"
        ">"
        "_CONSOLE;"
        "UNICODE;"
        "_UNICODE"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "ARM64")
    target_compile_definitions(${PROJECT_NAME} PRIVATE
        "$<$<CONFIG:AdHoc>:"
            "WIN32;"
            "_DEBUG"
        ">"
        "$<$<CONFIG:AppStore>:"
            "WIN32;"
            "_DEBUG"
        ">"
        "$<$<CONFIG:Debug>:"
            "_DEBUG"
        ">"
        "$<$<CONFIG:Release>:"
            "NDEBUG"
        ">"
        "_CONSOLE;"
        "UNICODE;"
        "_UNICODE"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "iPhone")
    target_compile_definitions(${PROJECT_NAME} PRIVATE
        "$<$<CONFIG:AdHoc>:"
            "WIN32;"
            "_DEBUG"
        ">"
        "$<$<CONFIG:AppStore>:"
            "WIN32;"
            "_DEBUG"
        ">"
        "$<$<CONFIG:Debug>:"
            "_DEBUG"
        ">"
        "$<$<CONFIG:Release>:"
            "NDEBUG"
        ">"
        "_CONSOLE;"
        "UNICODE;"
        "_UNICODE"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "iPhoneSimulator")
    target_compile_definitions(${PROJECT_NAME} PRIVATE
        "$<$<CONFIG:AdHoc>:"
            "WIN32;"
            "_DEBUG"
        ">"
        "$<$<CONFIG:AppStore>:"
            "WIN32;"
            "_DEBUG"
        ">"
        "$<$<CONFIG:Debug>:"
            "_DEBUG"
        ">"
        "$<$<CONFIG:Release>:"
            "NDEBUG"
        ">"
        "_CONSOLE;"
        "UNICODE;"
        "_UNICODE"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "x64")
    target_compile_definitions(${PROJECT_NAME} PRIVATE
        "$<$<CONFIG:AdHoc>:"
            "_DEBUG"
        ">"
        "$<$<CONFIG:AppStore>:"
            "_DEBUG"
        ">"
        "$<$<CONFIG:Debug>:"
            "_DEBUG"
        ">"
        "$<$<CONFIG:Release>:"
            "NDEBUG"
        ">"
        "_CONSOLE;"
        "UNICODE;"
        "_UNICODE"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "x86")
    target_compile_definitions(${PROJECT_NAME} PRIVATE
        "$<$<CONFIG:AdHoc>:"
            "_DEBUG"
        ">"
        "$<$<CONFIG:AppStore>:"
            "_DEBUG"
        ">"
        "$<$<CONFIG:Debug>:"
            "_DEBUG"
        ">"
        "$<$<CONFIG:Release>:"
            "NDEBUG"
        ">"
        "WIN32;"
        "_CONSOLE;"
        "UNICODE;"
        "_UNICODE"
    )
endif()

################################################################################
# Compile and link options
################################################################################
if(MSVC)
    if("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "Any CPU")
        target_compile_options(${PROJECT_NAME} PRIVATE
            $<$<CONFIG:Release>:
                /Oi;
                /Gy
            >
            /permissive-;
            /sdl;
            /W3;
            ${DEFAULT_CXX_DEBUG_INFORMATION_FORMAT};
            ${DEFAULT_CXX_EXCEPTION_HANDLING}
        )
    elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "ARM")
        target_compile_options(${PROJECT_NAME} PRIVATE
            $<$<CONFIG:Release>:
                /Oi;
                /Gy
            >
            /permissive-;
            /sdl;
            /W3;
            ${DEFAULT_CXX_DEBUG_INFORMATION_FORMAT};
            ${DEFAULT_CXX_EXCEPTION_HANDLING}
        )
    elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "ARM64")
        target_compile_options(${PROJECT_NAME} PRIVATE
            $<$<CONFIG:Release>:
                /Oi;
                /Gy
            >
            /permissive-;
            /sdl;
            /W3;
            ${DEFAULT_CXX_DEBUG_INFORMATION_FORMAT};
            ${DEFAULT_CXX_EXCEPTION_HANDLING}
        )
    elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "iPhone")
        target_compile_options(${PROJECT_NAME} PRIVATE
            $<$<CONFIG:Release>:
                /Oi;
                /Gy
            >
            /permissive-;
            /sdl;
            /W3;
            ${DEFAULT_CXX_DEBUG_INFORMATION_FORMAT};
            ${DEFAULT_CXX_EXCEPTION_HANDLING}
        )
    elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "iPhoneSimulator")
        target_compile_options(${PROJECT_NAME} PRIVATE
            $<$<CONFIG:Release>:
                /Oi;
                /Gy
            >
            /permissive-;
            /sdl;
            /W3;
            ${DEFAULT_CXX_DEBUG_INFORMATION_FORMAT};
            ${DEFAULT_CXX_EXCEPTION_HANDLING}
        )
    elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "x64")
        target_compile_options(${PROJECT_NAME} PRIVATE
            $<$<CONFIG:Release>:
                /Oi;
                /Gy
            >
            /permissive-;
            /sdl;
            /W3;
            ${DEFAULT_CXX_DEBUG_INFORMATION_FORMAT};
            ${DEFAULT_CXX_EXCEPTION_HANDLING}
        )
    elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "x86")
        target_compile_options(${PROJECT_NAME} PRIVATE
            $<$<CONFIG:Release>:
                /Oi;
                /Gy
            >
            /permissive-;
            /sdl;
            /W3;
            ${DEFAULT_CXX_DEBUG_INFORMATION_FORMAT};
            ${DEFAULT_CXX_EXCEPTION_HANDLING}
        )
    endif()
    if("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "Any CPU")
        target_link_options(${PROJECT_NAME} PRIVATE
            $<$<CONFIG:AdHoc>:
                /INCREMENTAL
            >
            $<$<CONFIG:AppStore>:
                /INCREMENTAL
            >
            $<$<CONFIG:Debug>:
                /INCREMENTAL
            >
            $<$<CONFIG:Release>:
                /OPT:REF;
                /OPT:ICF;
                /INCREMENTAL:NO
            >
            /DEBUG;
            /SUBSYSTEM:CONSOLE
        )
    elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "ARM")
        target_link_options(${PROJECT_NAME} PRIVATE
            $<$<CONFIG:AdHoc>:
                /INCREMENTAL
            >
            $<$<CONFIG:AppStore>:
                /INCREMENTAL
            >
            $<$<CONFIG:Debug>:
                /INCREMENTAL
            >
            $<$<CONFIG:Release>:
                /OPT:REF;
                /OPT:ICF;
                /INCREMENTAL:NO
            >
            /DEBUG;
            /SUBSYSTEM:CONSOLE
        )
    elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "ARM64")
        target_link_options(${PROJECT_NAME} PRIVATE
            $<$<CONFIG:AdHoc>:
                /INCREMENTAL
            >
            $<$<CONFIG:AppStore>:
                /INCREMENTAL
            >
            $<$<CONFIG:Debug>:
                /INCREMENTAL
            >
            $<$<CONFIG:Release>:
                /OPT:REF;
                /OPT:ICF;
                /INCREMENTAL:NO
            >
            /DEBUG;
            /SUBSYSTEM:CONSOLE
        )
    elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "iPhone")
        target_link_options(${PROJECT_NAME} PRIVATE
            $<$<CONFIG:AdHoc>:
                /INCREMENTAL
            >
            $<$<CONFIG:AppStore>:
                /INCREMENTAL
            >
            $<$<CONFIG:Debug>:
                /INCREMENTAL
            >
            $<$<CONFIG:Release>:
                /OPT:REF;
                /OPT:ICF;
                /INCREMENTAL:NO
            >
            /DEBUG;
            /SUBSYSTEM:CONSOLE
        )
    elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "iPhoneSimulator")
        target_link_options(${PROJECT_NAME} PRIVATE
            $<$<CONFIG:AdHoc>:
                /INCREMENTAL
            >
            $<$<CONFIG:AppStore>:
                /INCREMENTAL
            >
            $<$<CONFIG:Debug>:
                /INCREMENTAL
            >
            $<$<CONFIG:Release>:
                /OPT:REF;
                /OPT:ICF;
                /INCREMENTAL:NO
            >
            /DEBUG;
            /SUBSYSTEM:CONSOLE
        )
    elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "x64")
        target_link_options(${PROJECT_NAME} PRIVATE
            $<$<CONFIG:AdHoc>:
                /INCREMENTAL
            >
            $<$<CONFIG:AppStore>:
                /INCREMENTAL
            >
            $<$<CONFIG:Debug>:
                /INCREMENTAL
            >
            $<$<CONFIG:Release>:
                /OPT:REF;
                /OPT:ICF;
                /INCREMENTAL:NO
            >
            /DEBUG;
            /SUBSYSTEM:CONSOLE
        )
    elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "x86")
        target_link_options(${PROJECT_NAME} PRIVATE
            $<$<CONFIG:AdHoc>:
                /INCREMENTAL
            >
            $<$<CONFIG:AppStore>:
                /INCREMENTAL
            >
            $<$<CONFIG:Debug>:
                /INCREMENTAL
            >
            $<$<CONFIG:Release>:
                /OPT:REF;
                /OPT:ICF;
                /INCREMENTAL:NO
            >
            /DEBUG;
            /SUBSYSTEM:CONSOLE
        )
    endif()
endif()

if("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "Any CPU")
    target_link_directories(${PROJECT_NAME} PRIVATE
        "$<$<CONFIG:Debug>:"
            "D:/boost_1_77_0/stage/x64/lib"
        ">"
        "$<$<CONFIG:Release>:"
            "D:/boost_1_77_0/stage/x64/lib"
        ">"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "ARM")
    target_link_directories(${PROJECT_NAME} PRIVATE
        "$<$<CONFIG:Debug>:"
            "D:/boost_1_77_0/stage/x64/lib"
        ">"
        "$<$<CONFIG:Release>:"
            "D:/boost_1_77_0/stage/x64/lib"
        ">"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "ARM64")
    target_link_directories(${PROJECT_NAME} PRIVATE
        "$<$<CONFIG:Debug>:"
            "D:/boost_1_77_0/stage/x64/lib"
        ">"
        "$<$<CONFIG:Release>:"
            "D:/boost_1_77_0/stage/x64/lib"
        ">"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "iPhone")
    target_link_directories(${PROJECT_NAME} PRIVATE
        "$<$<CONFIG:Debug>:"
            "D:/boost_1_77_0/stage/x64/lib"
        ">"
        "$<$<CONFIG:Release>:"
            "D:/boost_1_77_0/stage/x64/lib"
        ">"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "iPhoneSimulator")
    target_link_directories(${PROJECT_NAME} PRIVATE
        "$<$<CONFIG:Debug>:"
            "D:/boost_1_77_0/stage/x64/lib"
        ">"
        "$<$<CONFIG:Release>:"
            "D:/boost_1_77_0/stage/x64/lib"
        ">"
    )
elseif("${CMAKE_VS_PLATFORM_NAME}" STREQUAL "x64")
    target_link_directories(${PROJECT_NAME} PRIVATE
        "D:/boost_1_77_0/stage/x64/lib"
    )
endif()

