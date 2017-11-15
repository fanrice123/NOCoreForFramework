using System;
using System.Collections.Generic;
using NetworkObservabilityCore.Utils;

namespace NetworkObservabilityCore.Algorithms
{
	/// <summary>
	/// An algorithm interface.
	/// </summary>
	/// <remarks>
	/// > [!Note]
	/// > <see cref="ConnectivityObserver"/> only works with algorithms
	/// > that implement this interface.
	/// </remarks>
	public interface IAlgorithm
	{

		/// <summary>
		/// The starting point of the algorithm.
		/// </summary>
		INode Source
		{
			get;
			set;
		}

		/// <summary>
		/// The <see cref="Constraint{T}"/> of <see cref="INode"/> to be used
		/// while running algorithm.
		/// </summary>
		Constraint<INode> NodeConstraint
		{
			get;
			set;
		}

		/// <summary>
		/// The <see cref="Constraint{T}"/> of <see cref="IEdge"/> to be used
		/// while running algorithm.
		/// </summary>
		Constraint<IEdge> EdgeConstraint
		{
			get;
			set;
		}

		/// <summary>
		/// The attribute to run algorithm.
		/// </summary>
		String SelectedEdgeAttribute
		{
			get;
			set;
		}

		/// <summary>
		/// Returns `true` if algorithm is set otherwise returns `false`.
		/// </summary>
		bool IsSet
		{
			get;
			set;
		}

		/// <summary>
		/// Set the data of the algorithm.
		/// </summary>
		/// <param name="src">See <see cref="Source"/>.</param>
		/// <param name="edgeAttr">See <see cref="SelectedEdgeAttribute"/>.</param>
		/// <param name="cNode">See <see cref="NodeConstraint"/>.</param>
		/// <param name="cEdge">See <see cref="EdgeConstraint"/>.</param>
		void Setup(INode src, String edgeAttr, Constraint<INode> cNode, Constraint<IEdge> cEdge);

		/// <summary>
		/// Executes algorithm. The algorithm must be set before calling this method.
		/// See also <seealso cref="Setup(INode, string, Constraint{INode}, Constraint{IEdge})"/>.
		/// </summary>
		/// <exception cref="InvalidOperationException">Throws if algorithm has not been set.</exception>
		void Run();

		/// <summary>
		/// Returns all the paths to the **node** given from <see cref="Source"/>.
		/// </summary>
		/// <param name="node">the destination point.</param>
		/// <returns>All possible paths.</returns>
		IList<Route> PathsTo(INode node);
	}
}
