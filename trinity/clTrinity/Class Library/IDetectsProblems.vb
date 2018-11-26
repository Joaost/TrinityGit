Public Interface IDetectsProblems

    Function DetectProblems() As List(Of Trinity.cProblem)

    Event ProblemsFound(ByVal problems As List(Of Trinity.cProblem))

End Interface
