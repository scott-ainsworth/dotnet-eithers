#!env bash
##
# Generate test coverage report
##

# Preparation

# dotnet tool install -g dotnet-coverage
# dotnet tool install -g dotnet-reportgenerator-globaltool

# Cleanup and Rebuild

dotnet clean
dotnet build

# Execution

mkdir -p coverage/history coverage/report

dotnet-coverage collect -f xml -o coverage/coverage.xml \
    dotnet test --verbosity minimal --results-directory . \
    --logger "html;logfilename=test-report.html;verbosity=detailed"
reportgenerator \
    -reports:'coverage/coverage.xml' \
    -targetdir:'coverage/report' \
    -reporttypes:'HtmlInline_AzurePipelines_Light;MarkdownSummary' \
    -historydir:'coverage/history' \
    -assemblyfilters:'-*.Tests.dll' \
    -verbosity:'Warning' \
    -title:'Ainsworth.Eithers Test Report'

# Open the report

# open test-report.html
open coverage/report/index.html

# end
