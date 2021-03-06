/*=========================================================================

  Program:   Visualization Toolkit
  Module:    $RCSfile: vtkProgrammableDataObjectSource.h,v $

  Copyright (c) Ken Martin, Will Schroeder, Bill Lorensen
  All rights reserved.
  See Copyright.txt or http://www.kitware.com/Copyright.htm for details.

     This software is distributed WITHOUT ANY WARRANTY; without even
     the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
     PURPOSE.  See the above copyright notice for more information.

=========================================================================*/
// .NAME vtkProgrammableDataObjectSource - generate source data object via a user-specified function
// .SECTION Description
// vtkProgrammableDataObjectSource is a source object that is programmable by
// the user. The output of the filter is a data object (vtkDataObject) which
// represents data via an instance of field data. To use this object, you
// must specify a function that creates the output.  
//
// Example use of this filter includes reading tabular data and encoding it
// as vtkFieldData. You can then use filters like vtkDataObjectToDataSetFilter
// to convert the data object to a dataset and then visualize it.  Another
// important use of this class is that it allows users of interpreters (e.g.,
// Tcl or Java) the ability to write source objects without having to
// recompile C++ code or generate new libraries.
// 
// .SECTION See Also
// vtkProgrammableFilter vtkProgrammableAttributeDataFilter
// vtkProgrammableSource vtkDataObjectToDataSetFilter

#ifndef __vtkProgrammableDataObjectSource_h
#define __vtkProgrammableDataObjectSource_h

#include "vtkDataObjectAlgorithm.h"

class VTK_GRAPHICS_EXPORT vtkProgrammableDataObjectSource : public vtkDataObjectAlgorithm
{
public:
  static vtkProgrammableDataObjectSource *New();
  vtkTypeRevisionMacro(vtkProgrammableDataObjectSource,vtkDataObjectAlgorithm);
  void PrintSelf(ostream& os, vtkIndent indent);

  // Description:
  // Specify the function to use to generate the output data object. Note
  // that the function takes a single (void *) argument.
  void SetExecuteMethod(void (*f)(void *), void *arg);

  // Description:
  // Set the arg delete method. This is used to free user memory.
  void SetExecuteMethodArgDelete(void (*f)(void *));

protected:
  vtkProgrammableDataObjectSource();
  ~vtkProgrammableDataObjectSource();

  virtual int RequestData(vtkInformation *, vtkInformationVector **,
                          vtkInformationVector *);

  void (*ExecuteMethod)(void *); //function to invoke
  void (*ExecuteMethodArgDelete)(void *);
  void *ExecuteMethodArg;
private:
  vtkProgrammableDataObjectSource(const vtkProgrammableDataObjectSource&);  // Not implemented.
  void operator=(const vtkProgrammableDataObjectSource&);  // Not implemented.
};

#endif
